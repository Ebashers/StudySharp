using System;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace StudySharp.ApplicationServices.JwtAuthService
{
    public static class JwtAuthServiceExtensions
    {
        public static IServiceCollection AddJwtAuthService(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtTokenConfig = configuration.GetSection(JwtTokenConfig.JwtTokenConfigSection).Get<JwtTokenConfig>();
            services.Configure<JwtTokenConfig>(options => configuration.GetSection(JwtTokenConfig.JwtTokenConfigSection).Bind(options));
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = jwtTokenConfig.Issuer,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtTokenConfig.Secret)),
                    ValidAudience = jwtTokenConfig.Audience,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                };
            });
            services.AddSingleton<IJwtService, JwtService>();
            services.AddHostedService<JwtRefreshTokenCache>();

            return services;
        }
    }
}
