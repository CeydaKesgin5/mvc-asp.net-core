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
    }
}
