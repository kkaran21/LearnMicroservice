using AutoMapper;
using CommandService.Data;
using CommandService.Dtos;
using CommandService.Models;
using Microsoft.AspNetCore.Mvc;

namespace CommandService.Controllers
{
    [Route("/c/[controller]")]
    [ApiController]
    public class PlatformsController : ControllerBase
    {
        private readonly ICommandRepo _commandRepo;
        private readonly IMapper _mapper;

        public PlatformsController(ICommandRepo commandRepo, IMapper mapper)
        {
            _commandRepo = commandRepo;
            _mapper = mapper;
        }
        
        [Route("getPlatforms")]
        [HttpGet]
        public ActionResult<IEnumerable<Platform>> getPlatforms()
        {
            Console.WriteLine("getting all platforms");
            var platforms = _commandRepo.getAllPlatforms();
            return Ok(_mapper.Map<IEnumerable<PlatformReadDto>>(platforms));
        }

        [HttpPost]
        public ActionResult TestInboundConnection(dynamic json)
        {   
            Console.WriteLine("hello"+json.ToString());
            return Ok("success");
        }
    }
}