using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.Data;
using PlatformService.Dtos;
using PlatformService.Models;

namespace PlatformService.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class PlatformController : ControllerBase
    {
        private readonly IPlatformRepo _repo;
        private readonly IMapper _mapper;
        public PlatformController(IPlatformRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;

        }

        [HttpGet]
        public ActionResult<IEnumerable<PlatformReadDto>> GetAllPlatforms()
        {
            var platforms = _repo.GetAllPlatforms();
            return Ok(_mapper.Map<IEnumerable<PlatformReadDto>>(platforms));
        }

        [HttpGet("{id}", Name ="getPlatformById")]
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
        public ActionResult<PlatformReadDto> createPlatform(PlatformCreateDto platformCreateDto)
        {
            var platformModel = _mapper.Map<Platform>(platformCreateDto);
            _repo.CreatePlatform(platformModel);
            _repo.SaveChanges();

            return CreatedAtRoute(nameof(getPlatformById),new {id=platformModel.Id}, platformModel);

        }




    }
}