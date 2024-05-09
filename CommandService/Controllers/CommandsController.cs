using AutoMapper;
using CommandService.Data;
using CommandService.Dtos;
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
            var Commands = _commandRepo.getCommandsForPlatform(platformId);
            return Ok(_mapper.Map<IEnumerable<CommandReadDto>>(Commands));
        }

        [Route("getCommandsById/{commandId}")]
        [HttpGet]
        public ActionResult<IEnumerable<CommandReadDto>> getCommandsById(int platformId,int commandId)
        {
            Console.WriteLine($"getCommandsById {platformId},{commandId}");
            var Commands = _commandRepo.getCommand(platformId,commandId);
            return Ok(_mapper.Map<IEnumerable<CommandReadDto>>(Commands));
        }

        


    }
}