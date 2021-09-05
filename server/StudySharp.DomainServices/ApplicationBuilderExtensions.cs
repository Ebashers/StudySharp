using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace StudySharp.DomainServices
{
    public static class ApplicationBuilderExtensions
    {
        public static void EnsureDbMigrated<T>(this IApplicationBuilder app)
            where T : DbContext
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            var context = serviceScope.ServiceProvider.GetService<T>();
            context?.Database?.Migrate();
        }
    }
}
