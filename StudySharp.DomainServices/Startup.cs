using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SqlKata.Compilers;
using StudySharp.Shared.Constants;

namespace StudySharp.DomainServices
{
    public static class Startup
    {
        public static IServiceCollection AddDomainServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<StudySharpDbContext>(
                options =>
                {
                    options.UseNpgsql(
                        configuration.GetConnectionString(ConnectionStrings.Default),
                        x => x.MigrationsAssembly(typeof(Startup).Assembly.GetName().FullName));
                }, ServiceLifetime.Transient)
                .AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<StudySharpDbContext>()
                .AddDefaultTokenProviders();

            services.AddScoped<Compiler, PostgresCompiler>();
            return services;
        }

        private static class Marker
        {
        }
    }
}
