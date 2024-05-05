using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PlatformService.Models;

namespace PlatformService.Data
{
    public static class PrepDb
    {
        public static void prepPopulation(IApplicationBuilder app, bool isProd)
        {

            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                seedData(serviceScope.ServiceProvider.GetService<AppDbContext>() ,isProd);
            }
        }

        private static void seedData(AppDbContext appDbContext, bool isProd)
        {
            if(isProd)
            {
                try
                {
                    Console.WriteLine("trying migrations");
                    appDbContext.Database.Migrate();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"exception occured while migrating {ex.Message}");
                }
            }
            if (!appDbContext.Platforms.Any())
            {
                appDbContext.Platforms.AddRange(
                    new Platform()
                    {
                        Name = "Dot Net",
                        Cost = "free",
                        Publisher = "Microsoft"
                    },
                    new Platform()
                    {
                        Name = "Sql Server Express",
                        Cost = "free",
                        Publisher = "hello"
                    },
                    new Platform()
                    {
                        Name = "Kubernetes",
                        Cost = "free",
                        Publisher = "hello"
                    }

                );

                appDbContext.SaveChanges();
                Console.WriteLine("Seeded Data Successfully");
            }
            else
            {
                Console.WriteLine("already have data");
            }
        }

    }


}