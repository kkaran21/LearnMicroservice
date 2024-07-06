using AutoMapper;
using CommandService.Data;
using CommandService.Models;
using Grpc.Net.Client;

namespace CommandService.SyncDataServices.Grpc
{
    public class PlatformDataClient : IPlatformDataClient
    {
        private readonly IConfiguration _configuration;

        public PlatformDataClient(IConfiguration configuration, IMapper mapper)
        {
            _configuration = configuration;
        }
        public IEnumerable<Platform> getAllPlatforms()
        {
            var channel = GrpcChannel.ForAddress(_configuration["GrpcServer"]);
            var client = new PlatformService.grpcPlatform.grpcPlatformClient(channel);
            var request = new PlatformService.emptyRequest();

            try
            {
                var response = client.getAllPlatforms(request);

                Console.WriteLine("GOt response from grpc server");

                List<Platform> platform = new List<Platform>();

                for (int i = 0; i < response.Platforms.Count; i++)
                {
                    Platform platform1 = new Platform
                    {
                        ExternalId = response.Platforms[i].PlatformId,
                        Name = response.Platforms[i].Name,
                    };

                    platform.Add(platform1);
                }

                return platform;
            }
            catch (System.Exception ex)
            {
                Console.WriteLine($"Could not get platfroms from grpcService {ex.Message}");
                return null;

            }

        }
    }
}