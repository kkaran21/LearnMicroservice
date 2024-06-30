using System.Text.Json;
using System.Text.Json.Serialization;
using AutoMapper;
using CommandService.Data;
using CommandService.Dtos;
using CommandService.Models;

namespace CommandService.EventProcessor
{
    public class EventProcessor : IEventProcessor
    {
        private readonly IMapper _mapper;
        private readonly IServiceScopeFactory _scopedService;

        public EventProcessor(IMapper mapper, IServiceScopeFactory scopedService)
        {
            _mapper = mapper;
            _scopedService = scopedService;
        }
        public void processEvent(string message)
        {
            var eventType = determineEvent(message);
            switch (eventType)
            {
                case EventType.PlatformPublished:
                    addPlatform(message);

                    break;
                default:
                    break;
            }
        }

        private EventType determineEvent(string message)
        {
            var eventType = JsonSerializer.Deserialize<GenericEventDto>(message);
            switch (eventType.Event)
            {
                case "Platform_Published":
                    return EventType.PlatformPublished;
                default:
                    return EventType.Undetermined;
            }
        }

        private void addPlatform(string message)
        {
            var platformPublishDto = JsonSerializer.Deserialize<PlatformPublishDto>(message);

            Console.WriteLine($"Message is {message}");
            Console.WriteLine($"platformPublishDto is {platformPublishDto.Name}");
            //                 var plat = _mapper.Map<Platform>(platformPublishDto);

            // Console.WriteLine($"platformPublishDto is {plat.Name},{plat.ExternalId}");

            using (var scope = _scopedService.CreateScope())
            {
                var repo = scope.ServiceProvider.GetRequiredService<ICommandRepo>();

                var plat = _mapper.Map<Platform>(platformPublishDto);

                try
                {
                    if (!repo.externalPlatformExists(plat.ExternalId))
                    {
                        repo.createPlatform(plat);
                        repo.SaveChanges();
                        Console.WriteLine(" event processed!!!");

                    }
                    else
                    {
                        Console.WriteLine("platform already exists");
                    }

                }
                catch (System.Exception e)
                {
                    Console.WriteLine(e.Message + "add platform method");
                }
            }

        }


        enum EventType
        {
            PlatformPublished,
            Undetermined
        }
    }
}