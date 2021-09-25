using System;
using System.Reflection;
using System.Text;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using StudySharp.ApplicationServices.EmailService;
using StudySharp.ApplicationServices.Infrastructure.EmailService;
using StudySharp.ApplicationServices.JwtAuthService;
using IValidationRule = StudySharp.Domain.Validators.IValidationRule;

namespace StudySharp.ApplicationServices
{
    public static class Startup
    {
        public static IServiceCollection AddApplicationServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.Scan(scanner =>
            {
                scanner
                    .FromAssemblyOf<Marker>()
                    .AddClasses(classes => classes.AssignableTo(typeof(IValidationRule)))
                    .UsingRegistrationStrategy(Scrutor.RegistrationStrategy.Skip)
                    .AsMatchingInterface()
                    .WithScopedLifetime();
            });
            var jwtTokenConfig = configuration.GetSection("jwtTokenConfig").Get<JwtTokenConfig>();
            services.AddSingleton(jwtTokenConfig);
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
            services.AddMediatR(Assembly.GetExecutingAssembly());
            return services;
        }

        public static IServiceCollection AddEmailService(this IServiceCollection services, IConfiguration configuration, string configurationSection)
        {
            services.Configure<EmailServiceSettings>(options => configuration.GetSection(configurationSection).Bind(options));
            services.AddScoped<IEmailService, MailKitEmailService>();

            return services;
        }

        private class Marker
        {
        }
    }
}
