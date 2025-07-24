using Microsoft.AspNetCore.Mvc;
using PokemonGameAPI.Contracts.DTOs.PokemonStats;
using PokemonGameAPI.Contracts.Services;

namespace PokemonGameAPI.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PokemonStatsController : ControllerBase
    {
        private readonly IPokemonStatsService _pokemonStatsService;

        public PokemonStatsController(IPokemonStatsService pokemonStatsService)
        {
            _pokemonStatsService = pokemonStatsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(int pageNumber = 1, int pageSize = 10)
        {
            var stats = await _pokemonStatsService.GetAllAsync(pageNumber, pageSize);
            if (stats == null)
            {
                return NotFound("No Pokemon stats found.");
            }
            return Ok(stats);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            if (id <= 0)
            {
                return BadRequest("ID must be greater than zero.");
            }
            var stats = await _pokemonStatsService.GetByIdAsync(id);
            if (stats == null)
            {
                return NotFound($"Pokemon with ID {id} not found.");
            }
            return Ok(stats);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PokemonStatsCreateDto model)
        {
            var createdStats = await _pokemonStatsService.CreateAsync(model);
            if (createdStats == null)
            {
                return BadRequest("Failed to create Pokemon stats.");
            }
            return CreatedAtAction(nameof(GetById), new { id = createdStats.Id }, createdStats);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] PokemonStatsUpdateDto model)
        {
            if (id <= 0)
            {
                return BadRequest("ID must be greater than zero.");
            }
            var updatedStats = await _pokemonStatsService.UpdateAsync(id, model);
            if (updatedStats == null)
            {
                return NotFound($"Pokemon stats with ID {id} not found.");
            }
            return Ok(updatedStats);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest("ID must be greater than zero.");
            }
            var deleted = await _pokemonStatsService.DeleteAsync(id);
            if (!deleted)
            {
                return NotFound($"Pokemon stats with ID {id} not found.");
            }
            return NoContent();

        }


    }
}
