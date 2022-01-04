using Mapster;
using Microsoft.AspNetCore.Mvc;
using PlatformService.Database.Models;
using PlatformService.Dtos;
using PlatformService.Implementation.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlatformService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatformsController : ControllerBase
    {
        private readonly IPlatformRepository _platformRepository;
        public PlatformsController(IPlatformRepository platformRepository)
        {
            _platformRepository = platformRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<PlatformReadDto>> GetPlatforms()
        {
            var platforms = await _platformRepository.GetAllPlatforms();

            return platforms.Adapt<IEnumerable<PlatformReadDto>>();
        }

        [HttpGet("{id}", Name = "GetPlatformById")]
        public async Task<ActionResult<PlatformReadDto>> GetPlatformById(int id)
        {
            var platform = await _platformRepository.GetPlatformById(id);

            if (platform != null)
            {
                return Ok(platform.Adapt<PlatformReadDto>());
            }

            return NotFound();
        }

        [HttpPost]
        public ActionResult<PlatformReadDto> CreatePlatform([FromBody] PlatformCreateDto platformCreateDto)
        {
            var platform = platformCreateDto.Adapt<Platform>();

            _platformRepository.CreatePlaform(platform);
            _platformRepository.SaveChanges();

            var platformReadDto = platform.Adapt<PlatformReadDto>();

            return CreatedAtRoute(nameof(GetPlatformById), new { Id = platformReadDto.Id }, platformReadDto);
        }

        // PUT api/<PlatformsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<PlatformsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
