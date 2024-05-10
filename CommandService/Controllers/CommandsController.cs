using AutoMapper;
using CommandService.Data;
using CommandService.Dtos;
using CommandService.Models;
using Microsoft.AspNetCore.Mvc;

namespace CommandService.Controllers
{
    [Route("/c/[controller]/{platformId}")]
    [ApiController]
    public class CommandsController : ControllerBase
    {
        private readonly ICommandRepo _commandRepo;
        private readonly IMapper _mapper;

        public CommandsController(ICommandRepo commandRepo, IMapper mapper)
        {
            _commandRepo = commandRepo;
            _mapper = mapper;
        }

        [Route("getCommandsForPlatform")]
        [HttpGet]
        public ActionResult<IEnumerable<CommandReadDto>> getCommandsForPlatform(int platformId)
        {
            Console.WriteLine($"getCommandsForPlatform {platformId}");
            if (!_commandRepo.platformExists(platformId))
            {
                return NotFound();
            }
            var Commands = _commandRepo.getCommandsForPlatform(platformId);
            return Ok(_mapper.Map<IEnumerable<CommandReadDto>>(Commands));
        }

        [Route("getCommandsById/{commandId}", Name = "getCommandsById")]
        [HttpGet]
        public ActionResult<CommandReadDto> getCommandsById(int platformId, int commandId)
        {
            Console.WriteLine($"getCommandsById {platformId},{commandId}");
            if (!_commandRepo.platformExists(platformId))
            {
                return NotFound();
            }
            var Commands = _commandRepo.getCommand(platformId, commandId);
            if (Commands == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<CommandReadDto>(Commands));
        }

        [Route("Command")]
        [HttpPost]
        public ActionResult<CommandReadDto> Command(int platformId, CommandCreateDto commandCreateDto)
        {
            Console.WriteLine($"post commands {commandCreateDto.CommandLine},{commandCreateDto.HowTo}");
            if (!_commandRepo.platformExists(platformId) && commandCreateDto != null)
            {
                return NotFound();
            }

            var commandModel = _mapper.Map<Command>(commandCreateDto);
            _commandRepo.createCommand(platformId, commandModel);
            _commandRepo.SaveChanges();
            var commandReadDto = _mapper.Map<CommandReadDto>(commandModel);

            return CreatedAtRoute("getCommandsById", new { platformId = platformId, commandId = commandModel.Id }, commandReadDto);
        }






    }
}