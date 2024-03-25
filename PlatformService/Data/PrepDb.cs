using Microsoft.IdentityModel.Tokens;
using PlatformService.Models;

namespace PlatformService.Data
{
    public static class PrepDb
    {
        public static void prepPopulation(IApplicationBuilder app)
        {

            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                seedData(serviceScope.ServiceProvider.GetService<AppDbContext>());
            }
        }

        private static void seedData(AppDbContext appDbContext)
        {
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