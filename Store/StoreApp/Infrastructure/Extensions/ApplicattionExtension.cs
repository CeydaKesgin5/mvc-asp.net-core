using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Repositories;

namespace StoreApp.Infrastructure.Extensions
{
    public static class ApplicattionExtension
    {
        public static void ConfigureAndCheckMigration(this IApplicationBuilder app)
        {
            RepositoryContext context = app.ApplicationServices.
                CreateScope().
                ServiceProvider.
                GetRequiredService<RepositoryContext>();

            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }
        }
               

            //bu işlem ile "database ef database update" komutuna ihtiyacımız kalmadı.

        public static void ConfigureLocalization(this WebApplication app)
        {
            app.UseRequestLocalization(options  =>
            {
                options.AddSupportedCultures("tr-TR")
                .AddSupportedUICultures("tr-TR")
                .SetDefaultCulture("tr-TR");
            });
        
        }


        public static async void ConfigureDefaultAdminUser(this IApplicationBuilder app) 
        {
            const string adminUser = "Admin";
            const string adminPassword = "password";


            UserManager<IdentityUser> userManager=app.ApplicationServices.CreateScope().ServiceProvider
                .GetRequiredService<UserManager<IdentityUser>>();


            RoleManager<IdentityRole> roleManager = app
                .ApplicationServices.CreateAsyncScope().ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            IdentityUser user= await userManager.FindByNameAsync(adminUser);

            if (user == null) {
                user = new IdentityUser()
                {
                    UserName = adminUser,
                    Email = "ceydaksgin@gmail.com",
                    PhoneNumber = "5555555555"
                };            
            }

            var result = await userManager.CreateAsync(user, adminPassword);
            if (!result.Succeeded) 
            {
                throw new Exception("admin user could not created.");
            }

            var roleResult = await userManager.AddToRolesAsync(user,
               roleManager.Roles.Select(r=>r.Name).ToList());

            if (!roleResult.Succeeded)
                throw new Exception("System have problems with role defination for admin");


        }
    }
}
