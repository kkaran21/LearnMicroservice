using CommandService.Models;

namespace CommandService.SyncDataServices.Grpc
{
    public interface IPlatformDataClient
    {
        public IEnumerable<Platform> getAllPlatforms();
    }
}