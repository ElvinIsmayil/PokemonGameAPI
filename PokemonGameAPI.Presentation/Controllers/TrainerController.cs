using Microsoft.AspNetCore.Mvc;
using PokemonGameAPI.Contracts.DTOs.Trainer;
using PokemonGameAPI.Contracts.Services;

namespace PokemonGameAPI.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainerController : ControllerBase
    {
        private readonly ITrainerService _trainerService;

        public TrainerController(ITrainerService trainerService)
        {
            _trainerService = trainerService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(int pageNumber = 1, int pageSize = 10)
        {
            var trainers = await _trainerService.GetAllAsync(pageNumber, pageSize);
            return Ok(trainers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var trainer = await _trainerService.GetByIdAsync(id);
            if (trainer == null)
            {
                return NotFound();
            }
            return Ok(trainer);
        }

        [HttpGet("trainer-pokemons")]
        public async Task<IActionResult> GetTrainerPokemons(int trainerId, int pageNumber = 1, int pageSize = 10)
        {
            var trainerPokemons = await _trainerService.GetTrainerPokemonsAsync(trainerId, pageNumber, pageSize);
            if (trainerPokemons == null)
            {
                return NotFound($"No Pokemons found for Trainer with ID {trainerId}.");
            }
            return Ok(trainerPokemons);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TrainerCreateDto model)
        {
            var createdTrainer = await _trainerService.CreateAsync(model);
            return CreatedAtAction(nameof(GetById), new { id = createdTrainer.Id }, createdTrainer);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] TrainerUpdateDto model)
        {
            var updatedTrainer = await _trainerService.UpdateAsync(id, model);
            if (updatedTrainer == null)
            {
                return NotFound();
            }
            return Ok(updatedTrainer);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _trainerService.DeleteAsync(id);
            if (!deleted)
            {
                return NotFound();
            }
            return NoContent();

        }

        [HttpPost("assign-starter")]
        public async Task<IActionResult> AssignStarterPokemon([FromBody] AssignPokemonDto model)
        {
            await _trainerService.ChooseStarterPokemonAsync(model);
            return Ok("Starter Pokemon assigned successfully.");
        }

        [HttpPost("assign-starter-pokemon")]
        public async Task<IActionResult> AssignPokemonAsync([FromBody] AssignPokemonDto model)
        {
            await _trainerService.AssignPokemonToTrainerAsync(model);
            return Ok("Pokemon Assigned successfully.");

        }

        [HttpPost("assign-pokemon")]
        public async Task<IActionResult> AssignPokemonToTrainerAsync([FromBody] AssignPokemonDto model)
        {
            await _trainerService.AssignPokemonToTrainerAsync(model);
            return Ok("Pokemon assigned to trainer successfully.");
        }
    }
}
