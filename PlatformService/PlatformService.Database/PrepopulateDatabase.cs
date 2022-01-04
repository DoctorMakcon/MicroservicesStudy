using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using PlatformService.Database.Contexts;
using PlatformService.Database.Models;
using System.Linq;

namespace PlatformService.Database
{
    public static class PrepopulateDatabase
    {
        public static void Prepopulate(IApplicationBuilder builder)
        {
            using (var scope = builder.ApplicationServices.CreateScope())
            {
                SeedData(scope.ServiceProvider.GetService<AppDbContext>());
            }
        }

        private static void SeedData(AppDbContext context)
        {
            if (!context.Platforms.Any())
            {
                context.Platforms.AddRange(
                    new Platform { Name = "Name1", Publisher = "Publisher1", Cost = "Free" },
                    new Platform { Name = "Name2", Publisher = "Publisher2", Cost = "Free" },
                    new Platform { Name = "Name3", Publisher = "Publisher3", Cost = "Free" },
                    new Platform { Name = "Name4", Publisher = "Publisher4", Cost = "Free" });

                context.SaveChanges();
            }
        }
    }
}
