using System.Reflection;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
            services.AddJwtAuthService(configuration);
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
