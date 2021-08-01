using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
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
            var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL");
            var databaseUri = new Uri(databaseUrl);
            var userInfo = databaseUri.UserInfo.Split(':');

            var builder = new NpgsqlConnectionStringBuilder
            {
                Host = databaseUri.Host,
                Port = databaseUri.Port,
                Username = userInfo[0],
                Password = userInfo[1],
                Database = databaseUri.LocalPath.TrimStart('/'),
                SslMode = SslMode.Require,
                TrustServerCertificate = true
            };
            services.AddDbContext<StudySharpDbContext>(
                options =>
                {
                    options.UseNpgsql(builder.ToString(),
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
