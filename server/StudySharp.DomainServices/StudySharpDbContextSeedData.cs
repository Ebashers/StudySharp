using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StudySharp.Domain.Enums;

namespace StudySharp.DomainServices
{
    public class StudySharpDbContextSeedData
    {
         public static async Task Initialize(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetService<StudySharpDbContext>();
                var userManager = scope.ServiceProvider.GetService<UserManager<ApplicationUser>>();
                var roleManager = scope.ServiceProvider.GetService<RoleManager<IdentityRole>>();
                if (!await context.Roles.AnyAsync())
                {
                    GenerateAndSeedRoles(roleManager);
                }

                var email = configuration.GetValue<string>("AdminUserEmail");
                var password = configuration.GetValue<string>("AdminUserPswd");

                var user = new ApplicationUser
                {
                    Email = email.ToLower(),
                    UserName = email.ToLower(),
                };

                if (!context.Users.Any(u => u.NormalizedUserName == email.ToUpper()))
                {
                    var res = await userManager.CreateAsync(user, password);
                    if (res.Succeeded)
                    {
                        var adminRole = new IdentityRole("Admin");
                        if (!context.Roles.Any(e => e.NormalizedName == adminRole.Name.ToUpper()))
                        {
                            var res1 = await roleManager.CreateAsync(adminRole);
                            if (res1.Succeeded)
                            {
                                var adminUser = context.Users.Find(user.Id);
                                await userManager.AddToRoleAsync(adminUser, adminRole.Name);
                            }
                        }

                        try
                        {
                            context.Users.Add(new ApplicationUser { Email = user.Email, UserName = user.UserName });
                            await context.SaveChangesAsync();
                        }
                        catch (Exception)
                        {
                            throw new ApplicationException();
                        }
                    }
                }
            }
        }

         private static async void GenerateAndSeedRoles(RoleManager<IdentityRole> roleManager)
         {
             if (roleManager.Roles.Any())
             {
                 return;
             }

             foreach (var name in Enum.GetNames<DomainRoles>())
             {
                 await roleManager.CreateAsync(new IdentityRole(name));
             }
         }
    }
}