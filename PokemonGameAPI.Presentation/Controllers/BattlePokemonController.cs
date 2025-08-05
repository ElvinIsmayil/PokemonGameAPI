using Microsoft.AspNetCore.Mvc;
using PokemonGameAPI.Contracts.DTOs.BattlePokemon;
using PokemonGameAPI.Contracts.Services;

namespace PokemonGameAPI.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BattlePokemonController : ControllerBase
    {
        private readonly IBattlePokemonService _battlePokemonService;

        public BattlePokemonController(IBattlePokemonService battlePokemonService)
        {
            _battlePokemonService = battlePokemonService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(int pageNumber = 1, int pageSize = 10)
        {
            var battlePokemons = await _battlePokemonService.GetAllAsync(pageNumber, pageSize);
            return Ok(battlePokemons);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var battlePokemon = await _battlePokemonService.GetByIdAsync(id);
            if (battlePokemon == null)
                return NotFound();

            return Ok(battlePokemon);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] BattlePokemonCreateDto model)
        {
            var createdBattlePokemon = await _battlePokemonService.CreateAsync(model);
            return CreatedAtAction(nameof(GetById), new { id = createdBattlePokemon.Id }, createdBattlePokemon);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] BattlePokemonUpdateDto model)
        {
            var updatedBattlePokemon = await _battlePokemonService.UpdateAsync(id, model);
            if (updatedBattlePokemon == null)
                return NotFound();

            return Ok(updatedBattlePokemon);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _battlePokemonService.DeleteAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
