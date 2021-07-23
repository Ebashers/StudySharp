using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StudySharp.DomainServices;
using StudySharp.Shared.Constants;

namespace StudySharp.Teacher
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<StudySharpDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString(ConnectionStrings.Default),
                assembly => assembly.MigrationsAssembly(typeof(StudySharpDbContext).Assembly.FullName)))
                .AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<StudySharpDbContext>()
                .AddDefaultTokenProviders();
            services.AddAuthorization(opt => opt.AddPolicy(AuthorizationPolicies.TeacherPolicy, policy => policy.RequireRole(nameof(DomainRoles.Teacher))));
            services.AddRazorPages().AddRazorPagesOptions(config =>
            {
                config.Conventions.AuthorizeFolder("/Teacher", AuthorizationPolicies.TeacherPolicy);
            }).AddRazorRuntimeCompilation();
            services.ConfigureApplicationCookie(opt => opt.LoginPath = RedirectUrls.Unauthorized);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
