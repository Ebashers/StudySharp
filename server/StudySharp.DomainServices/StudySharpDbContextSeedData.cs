using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StudySharp.Domain.Enums;

namespace StudySharp.DomainServices
{
    public static class StudySharpDbContextSeedData
    {
        private const string AdminCredentialsSection = "AdminCredentials";
        private const string AdminUserName = "UserName";
        private const string AdminPassword = "Password";

        public static async Task InitializeAsync(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            using var scope = serviceProvider.CreateScope();

            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<int>>>();

            await GenerateAndSeedRoles(roleManager);
            await GenerateAndSeedAdmin(userManager, configuration);
        }

        private static async Task GenerateAndSeedRoles(RoleManager<IdentityRole<int>> roleManager)
        {
            if (await roleManager.Roles.AnyAsync())
            {
                return;
            }

            foreach (var roleName in Enum.GetNames<DomainRoles>())
            {
                await roleManager.CreateAsync(new IdentityRole<int>(roleName));
            }
        }

        private static async Task GenerateAndSeedAdmin(UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            var userName = configuration.GetSection(AdminCredentialsSection).GetValue<string>(AdminUserName);
            var password = configuration.GetSection(AdminCredentialsSection).GetValue<string>(AdminPassword);

            if (await userManager.Users.AnyAsync(_ => _.NormalizedUserName.Equals(userName.ToUpper())))
            {
                return;
            }

            var admin = new ApplicationUser
            {
                Email = userName,
                UserName = userName,
            };

            await userManager.CreateAsync(admin, password);
            await userManager.AddToRoleAsync(admin, DomainRoles.Admin.ToString());
        }
    }
}