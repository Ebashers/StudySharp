using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using StudySharp.Domain.Constants;

namespace StudySharp.ApplicationServices.JwtAuthService
{
    public interface IJwtService
    {
        IImmutableDictionary<string, RefreshToken> UsersRefreshTokensReadOnlyDictionary { get; }
        JwtAuthResult GenerateTokens(string username, List<Claim> claims, DateTime utcNow);
        JwtAuthResult Refresh(string refreshToken, string accessToken, List<Claim> claims, DateTime utcNow);
        void RemoveExpiredRefreshTokens(DateTime utcNow);
        void RemoveRefreshTokenByUserName(string userName);
        (ClaimsPrincipal principal, JwtSecurityToken?) DecodeJwtToken(string token);
    }

    public class JwtService : IJwtService
    {
        public IImmutableDictionary<string, RefreshToken> UsersRefreshTokensReadOnlyDictionary => _usersRefreshTokens.ToImmutableDictionary();
        private readonly ConcurrentDictionary<string, RefreshToken> _usersRefreshTokens;  // can store in a database or a distributed cache
        private readonly JwtTokenConfig _jwtTokenConfig;
        private readonly byte[] _secret;

        public JwtService(IOptions<JwtTokenConfig> jwtTokenConfigOptions)
        {
            _jwtTokenConfig = jwtTokenConfigOptions.Value;
            _usersRefreshTokens = new ConcurrentDictionary<string, RefreshToken>();
            _secret = Encoding.ASCII.GetBytes(jwtTokenConfigOptions.Value.Secret);
        }

        // optional: clean up expired refresh tokens
        public void RemoveExpiredRefreshTokens(DateTime utcNow)
        {
            var expiredTokens = _usersRefreshTokens.Where(x => x.Value.ExpireAt < utcNow).ToList();
            foreach (var expiredToken in expiredTokens)
            {
                _usersRefreshTokens.TryRemove(expiredToken.Key, out _);
            }
        }

        // can be more specific to ip, user agent, device name, etc.
        public void RemoveRefreshTokenByUserName(string userName)
        {
            var refreshTokens = _usersRefreshTokens.Where(x => x.Value.UserName == userName).ToList();
            foreach (var refreshToken in refreshTokens)
            {
                _usersRefreshTokens.TryRemove(refreshToken.Key, out _);
            }
        }

        public JwtAuthResult GenerateTokens(string username, List<Claim> claims, DateTime utcNow)
        {
            var shouldAddAudienceClaim = string.IsNullOrWhiteSpace(claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Aud)?.Value);
            var jwtToken = new JwtSecurityToken(
                _jwtTokenConfig.Issuer,
                shouldAddAudienceClaim ? _jwtTokenConfig.Audience : string.Empty,
                claims,
                expires: utcNow.AddMinutes(_jwtTokenConfig.AccessTokenExpiration),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(_secret), SecurityAlgorithms.HmacSha256Signature));
            var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);

            var refreshToken = new RefreshToken
            {
                UserName = username,
                TokenString = GenerateRefreshTokenString(),
                ExpireAt = utcNow.AddMinutes(_jwtTokenConfig.RefreshTokenExpiration),
            };
            _usersRefreshTokens.AddOrUpdate(refreshToken.TokenString, refreshToken, (_, _) => refreshToken);

            return new JwtAuthResult
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
            };
        }

        public JwtAuthResult Refresh(string refreshToken, string accessToken, List<Claim> claims, DateTime utcNow)
        {
            var (principal, jwtToken) = DecodeJwtToken(accessToken);
            if (jwtToken == null || !jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256Signature))
            {
                throw new SecurityTokenException(ErrorConstants.InvalidToken);
            }

            var userName = principal.Identity?.Name;
            if (!_usersRefreshTokens.TryGetValue(refreshToken, out var existingRefreshToken))
            {
                throw new SecurityTokenException(ErrorConstants.InvalidToken);
            }

            if (existingRefreshToken.UserName != userName || existingRefreshToken.ExpireAt < utcNow)
            {
                throw new SecurityTokenException(ErrorConstants.InvalidToken);
            }

            return GenerateTokens(userName, claims, utcNow);
        }

        public (ClaimsPrincipal principal, JwtSecurityToken?) DecodeJwtToken(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
            {
                throw new SecurityTokenException(ErrorConstants.InvalidToken);
            }

            var principal = new JwtSecurityTokenHandler()
                .ValidateToken(
                    token,
                    new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = _jwtTokenConfig.Issuer,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(_secret),
                        ValidAudience = _jwtTokenConfig.Audience,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero,
                    },
                    out var validatedToken);
            return (principal, validatedToken as JwtSecurityToken);
        }

        private static string GenerateRefreshTokenString()
        {
            var randomNumber = new byte[32];
            using var randomNumberGenerator = RandomNumberGenerator.Create();
            randomNumberGenerator.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }

    public class JwtAuthResult
    {
        [JsonPropertyName("accessToken")]
        public string AccessToken { get; set; }

        [JsonPropertyName("refreshToken")]
        public RefreshToken RefreshToken { get; set; }
    }

    public class RefreshToken
    {
        [JsonPropertyName("username")]
        public string UserName { get; set; }

        // can be used for usage tracking
        // can optionally include other metadata, such as user agent, ip address, device name, and so on
        [JsonPropertyName("tokenString")]
        public string TokenString { get; set; }

        [JsonPropertyName("expireAt")]
        public DateTime ExpireAt { get; set; }
    }
}
