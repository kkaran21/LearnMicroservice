using PlatformService.Dtos;

namespace PlatformService.AsyncDataServices
{
    public interface IMessageBusClient
    {
        void PublishToPlatform(PlatformPublishDto platformPublishDto);
    }
}