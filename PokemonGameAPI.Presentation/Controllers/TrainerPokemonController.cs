using Microsoft.AspNetCore.Mvc;
using PokemonGameAPI.Contracts.DTOs.TrainerPokemon;
using PokemonGameAPI.Contracts.Services;

namespace PokemonGameAPI.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainerPokemonController : ControllerBase
    {
        private readonly ITrainerPokemonService _trainerPokemonService;

        public TrainerPokemonController(ITrainerPokemonService trainerPokemonService)
        {
            _trainerPokemonService = trainerPokemonService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(int pageNumber = 1, int pageSize = 10)
        {
            var trainerPokemons = await _trainerPokemonService.GetAllAsync(pageNumber, pageSize);
            return Ok(trainerPokemons);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var trainerPokemon = await _trainerPokemonService.GetByIdAsync(id);
            if (trainerPokemon == null)
            {
                return NotFound();
            }
            return Ok(trainerPokemon);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TrainerPokemonCreateDto model)
        {
            var createdTrainerPokemon = await _trainerPokemonService.CreateAsync(model);
            return CreatedAtAction(nameof(GetById), new { id = createdTrainerPokemon.Id }, createdTrainerPokemon);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] TrainerPokemonUpdateDto model)
        {
            var updatedTrainerPokemon = await _trainerPokemonService.UpdateAsync(id, model);
            if (updatedTrainerPokemon == null)
            {
                return NotFound();
            }
            return Ok(updatedTrainerPokemon);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _trainerPokemonService.DeleteAsync(id);
            if (!deleted)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpPost("{id}/levelup")]
        public async Task<IActionResult> LevelUp(LevelUpDto model)
        {
            await _trainerPokemonService.LevelUpAsync(model);
            return Ok();
        }

        // Uncomment and implement when ready
        // [HttpPost("{id}/evolve")]
        // public async Task<IActionResult> Evolve(int id)
        // {
        //     var evolved = await _trainerPokemonService.EvolveAsync(id);
        //     if (evolved == null)
        //     {
        //         return NotFound();
        //     }
        //     return Ok(evolved);
        // }
    }
}
