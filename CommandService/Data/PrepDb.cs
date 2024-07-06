using CommandService.Models;
using CommandService.SyncDataServices.Grpc;
using Microsoft.EntityFrameworkCore;


namespace CommandService.Data
{
    public static class PrepDb
    {
        public static void prepPopulation(IApplicationBuilder app)
        {

            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var grpcClient = serviceScope.ServiceProvider.GetRequiredService<IPlatformDataClient>();
                var Platforms = grpcClient.getAllPlatforms();
                var commandRepo = serviceScope.ServiceProvider.GetRequiredService<ICommandRepo>();

                seedData(Platforms,commandRepo);
            }
        }

        private static void seedData(IEnumerable<Platform> platforms, ICommandRepo commandRepo)
        {
            foreach (var item in platforms)
            {
                if(!commandRepo.externalPlatformExists(item.ExternalId))
                {
                    commandRepo.createPlatform(item);
                }
            }
        }



    }
}
