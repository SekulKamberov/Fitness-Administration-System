namespace FAS.Web.Infrastructure.Extensions
{

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.DependencyInjection;

    using FAS.Data;
    using FAS.Data.Models;

    public static class ApplicationBuilderExtensions
    {
        private const string FirstName = "Sekul";
        private const string LastName = "Kamberov";
        private const string AdminEmail = "se4ko@abv.bg";
        private const string AdminPass = "369";

        public static IApplicationBuilder UseDatabaseMigration(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope.ServiceProvider.GetService<FASDbContext>().Database.Migrate();

                var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<IdentityRole>>();
                var userManager = serviceScope.ServiceProvider.GetService<UserManager<User>>();

                Task.Run(async () =>
                    {
                        // Seed Roles
                        var roles = new[]
                        {
                            WebConstants.AdministratorRole,
                            WebConstants.GymInspector,
                            WebConstants.TrainerRole
                        };

                        foreach (var role in roles)
                        {
                            var roleExists = await roleManager.RoleExistsAsync(role);

                            if (!roleExists)
                            {
                                await roleManager.CreateAsync(new IdentityRole
                                {
                                    Name = role
                                });
                            }
                        }

                        // Seed Admin User
                        var adminUser = await userManager.FindByEmailAsync(AdminEmail);

                        if (adminUser == null)
                        {
                            adminUser = new User
                            {
                                FirstName = FirstName,
                                LastName = LastName,
                                ProofGiven = 1,
                                Age = 37,
                                Sex = Sex.Female,
                                UserPhoto = "sekul.jpg",
                                UserName = "se4ko@abv.bg",
                                Date = new DateTime(2015, 05, 15),
                                Adress = "Sofia",
                                PhoneNumber = "+359 899 750 224",
                                Height = 181,
                                Weight = 105,
                                MembershipType = MembershipType.KohINoor,                             
                                Email = AdminEmail,
                                IsPayed = true,
                                IsStarMember = true,
                                IsBlocked = false
                            };

                            var result = await userManager.CreateAsync(adminUser, AdminPass);

                            // Add User to Role
                            if (result.Succeeded)
                            {
                                await userManager.AddToRoleAsync(adminUser, WebConstants.AdministratorRole);
                            }
                        }
                        else
                        {
                            // Add User to Role
                            await userManager.AddToRoleAsync(adminUser, WebConstants.AdministratorRole);
                        }
                    })
                    .GetAwaiter()
                    .GetResult();
            }

            return app;
        }
    }
}
