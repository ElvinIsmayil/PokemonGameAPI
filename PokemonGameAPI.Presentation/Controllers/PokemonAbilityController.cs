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
        public async Task<IActionResult> Create([FromBody] PokemonAbilityRequestDto model)
        {
            var createdAbility = await _pokemonAbilityService.CreateAsync(model);
            return CreatedAtAction(nameof(GetById), new { id = createdAbility.Id }, createdAbility);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] PokemonAbilityRequestDto model)
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

        [HttpPost("assing-ability")]
        public async Task<IActionResult> AssignAbilityToPokemon([FromBody] PokemonAbilityAssignDto model)
        {
            var result = await _pokemonAbilityService.AssignPokemonAbility(model);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);

        }

        [HttpGet("pokemon-abilities/{pokemonId}")]
        public async Task<IActionResult> GetPokemonAbilities(int pokemonId)
        {
            var abilities = await _pokemonAbilityService.GetAllAbilitiesByPokemonIdAsync(pokemonId);
            if (abilities == null)
            {
                return NotFound();
            }
            return Ok(abilities);
        }
    }
}
