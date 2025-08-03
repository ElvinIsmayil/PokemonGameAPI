using Microsoft.AspNetCore.Mvc;
using PokemonGameAPI.Contracts.DTOs.TrainerPokemonStats;
using PokemonGameAPI.Contracts.Services;

namespace PokemonGameAPI.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainerPokemonStatsController : ControllerBase
    {
        private readonly ITrainerPokemonStatsService _trainerPokemonStatsService;

        public TrainerPokemonStatsController(ITrainerPokemonStatsService trainerPokemonStatsService)
        {
            _trainerPokemonStatsService = trainerPokemonStatsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(int pageNumber = 1, int pageSize = 10)
        {
            var result = await _trainerPokemonStatsService.GetAllAsync(pageNumber, pageSize);
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _trainerPokemonStatsService.GetByIdAsync(id);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TrainerPokemonStatsCreateDto model)
        {
            var result = await _trainerPokemonStatsService.CreateAsync(model);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] TrainerPokemonStatsUpdateDto model)
        {
            var result = await _trainerPokemonStatsService.UpdateAsync(id, model);
            return Ok(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest("ID must be greater than zero");
            }
            var result = await _trainerPokemonStatsService.DeleteAsync(id);
            if (!result)
            {
                return NotFound($"Entity with ID {id} not found");
            }
            return NoContent();
        }

    }
}
