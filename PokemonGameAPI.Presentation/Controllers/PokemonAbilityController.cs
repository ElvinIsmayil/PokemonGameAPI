using Microsoft.AspNetCore.Mvc;
using PokemonGameAPI.Contracts.DTOs.PokemonAbility;
using PokemonGameAPI.Contracts.Services;

namespace PokemonGameAPI.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PokemonAbilityController : ControllerBase
    {
        private readonly IPokemonAbilityService _pokemonAbilityService;

        public PokemonAbilityController(IPokemonAbilityService pokemonAbilityService)
        {
            _pokemonAbilityService = pokemonAbilityService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(int pageNumber = 1, int pageSize = 10)
        {
            var abilities = await _pokemonAbilityService.GetAllAsync(pageNumber, pageSize);
            return Ok(abilities);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var ability = await _pokemonAbilityService.GetByIdAsync(id);
            if (ability == null)
            {
                return NotFound();
            }
            return Ok(ability);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PokemonAbilityCreateDto model)
        {
            var createdAbility = await _pokemonAbilityService.CreateAsync(model);
            return CreatedAtAction(nameof(GetById), new { id = createdAbility.Id }, createdAbility);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] PokemonAbilityUpdateDto model)
        {
            var updatedAbility = await _pokemonAbilityService.UpdateAsync(id, model);
            if (updatedAbility == null)
            {
                return NotFound();
            }
            return Ok(updatedAbility);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _pokemonAbilityService.DeleteAsync(id);
            if (!deleted)
            {
                return NotFound();
            }
            return NoContent();

        }

    }
}
