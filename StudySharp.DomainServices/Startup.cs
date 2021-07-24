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
            services.AddDbContext<StudySharpDbContext>(options => options.UseSqlServer(configuration.GetConnectionString(ConnectionStrings.Default),
                    assembly => assembly.MigrationsAssembly(typeof(StudySharpDbContext).Assembly.FullName)))
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
