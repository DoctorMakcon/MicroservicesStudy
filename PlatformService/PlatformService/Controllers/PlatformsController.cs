using Mapster;
using Microsoft.AspNetCore.Mvc;
using PlatformService.Database.Models;
using PlatformService.Implementation.Dtos;
using PlatformService.Implementation.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlatformService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatformsController : ControllerBase
    {
        private readonly IPlatformRepository _platformRepository;
        private readonly ICommandDataClient _commandDataClient;

        public PlatformsController(IPlatformRepository platformRepository, ICommandDataClient commandDataClient)
        {
            _platformRepository = platformRepository;
            _commandDataClient = commandDataClient;
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
        public async Task<ActionResult<PlatformReadDto>> CreatePlatform([FromBody] PlatformCreateDto platformCreateDto)
        {
            var platform = platformCreateDto.Adapt<Platform>();

            _platformRepository.CreatePlaform(platform);
            await _platformRepository.SaveChanges();

            var platformReadDto = platform.Adapt<PlatformReadDto>();

            try
            {
                await _commandDataClient.SendPlatformToCommandService(platformReadDto);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"---> Could not send synchronously: {ex.Message}");
            }

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
