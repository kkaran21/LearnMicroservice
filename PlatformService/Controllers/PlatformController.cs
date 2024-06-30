using System.Windows.Input;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.AsyncDataServices;
using PlatformService.Data;
using PlatformService.Dtos;
using PlatformService.Models;
using PlatformService.SyncDataServices.Http;

namespace PlatformService.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class PlatformController : ControllerBase
    {
        private readonly IMessageBusClient _messageBusClient;
        private readonly IPlatformRepo _repo;
        private readonly IMapper _mapper;
        private readonly ICommandDataClient _commandDataClient;

        public PlatformController(IPlatformRepo repo, IMapper mapper, ICommandDataClient commandDataClient,IMessageBusClient messageBusClient)
        {
            _messageBusClient = messageBusClient;
            _repo = repo;
            _mapper = mapper;
            _commandDataClient = commandDataClient;

        }

        [HttpGet]
        public ActionResult<IEnumerable<PlatformReadDto>> GetAllPlatforms()
        {
            var platforms = _repo.GetAllPlatforms();
            return Ok(_mapper.Map<IEnumerable<PlatformReadDto>>(platforms));
        }

        [HttpGet("{id}", Name = "getPlatformById")]
        public ActionResult<PlatformReadDto> getPlatformById(int id)
        {
            var Platform = _repo.getPlatformById(id);
            if (Platform != null)
            {
                return Ok(_mapper.Map<PlatformReadDto>(Platform));

            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<ActionResult<PlatformReadDto>> createPlatform(PlatformCreateDto platformCreateDto)
        {
            var platformModel = _mapper.Map<Platform>(platformCreateDto);
            _repo.CreatePlatform(platformModel);
            _repo.SaveChanges();

            var PlatformReadDto = _mapper.Map<PlatformReadDto>(platformModel);

            ////sync message using httpclient
            try
            {
                 await _commandDataClient.SendPlatformToCommand(PlatformReadDto); 

            }
            catch (Exception ex)
            {
                Console.WriteLine($"failed {ex.Message}");
            }

            ////async message using messagebus
            try
            {
                var message = _mapper.Map<PlatformPublishDto>(PlatformReadDto);
                message.Event = "Platform_Published";
                _messageBusClient.PublishToPlatform(message);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"failed {ex.Message}");
            }

            return CreatedAtRoute(nameof(getPlatformById), new { id = platformModel.Id }, platformModel);

        }




    }
}