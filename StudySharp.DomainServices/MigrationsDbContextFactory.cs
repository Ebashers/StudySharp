using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Npgsql;

namespace StudySharp.DomainServices
{
    internal class MigrationsDbContextFactory : IDesignTimeDbContextFactory<StudySharpDbContext>
    {
        public StudySharpDbContext CreateDbContext(string[] args)
        {
            var connectionString = args.Any()
                ? args[0]
                : GetConnectionString();
            Console.WriteLine("xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx");
            Console.WriteLine(connectionString);
            var builder = new DbContextOptionsBuilder<StudySharpDbContext>();
            builder
                .UseNpgsql(connectionString);
            var context = new StudySharpDbContext(builder.Options);
            return context;
        }

        private string GetConnectionString()
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
            return builder.ToString();
        }
    }
}
