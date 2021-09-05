using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.Linq;

namespace StudySharp.DomainServices
{
    internal class MigrationsDbContextFactory : IDesignTimeDbContextFactory<StudySharpDbContext>
    {
        public StudySharpDbContext CreateDbContext(string[] args)
        {
            var connectionString = args.Any()
                ? args[0]
                : @"User ID=postgres;Password=postgres;Host=localhost;Port=5432;Database=Assessment";

            var builder = new DbContextOptionsBuilder<StudySharpDbContext>();
            builder
                .UseNpgsql(connectionString);
            var context = new StudySharpDbContext(builder.Options);
            return context;
        }
    }
}
