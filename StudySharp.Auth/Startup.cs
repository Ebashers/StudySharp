using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StudySharp.DomainServices;
using StudySharp.Shared.Constants;

namespace StudySharp.Auth
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages().AddRazorPagesOptions(config => { config.Conventions.AuthorizePage("/Privacy"); })
                .AddRazorRuntimeCompilation();
            services.ConfigureApplicationCookie(opt => opt.LoginPath = RedirectUrls.Unauthorized);
            services.AddDomainServices(Configuration);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles()
                .UseRouting()
                .UseAuthentication()
                .UseAuthorization()
                .UseEndpoints(endpoints => { endpoints.MapRazorPages(); })
                .EnsureDbMigrated<StudySharpDbContext>();
        }
    }
}
