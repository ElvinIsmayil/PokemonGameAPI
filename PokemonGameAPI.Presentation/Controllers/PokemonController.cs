using Microsoft.AspNetCore.Mvc;
using PokemonGameAPI.Contracts.DTOs.Pokemon;
using PokemonGameAPI.Contracts.Services;

namespace PokemonGameAPI.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PokemonController : ControllerBase
    {
        private readonly IPokemonService _pokemonService;

        public PokemonController(IPokemonService pokemonService)
        {
            _pokemonService = pokemonService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(int pageNumber = 1, int pageSize = 10)
        {
            var pokemons = await _pokemonService.GetAllAsync(pageNumber, pageSize);
            return Ok(pokemons);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var pokemon = await _pokemonService.GetByIdAsync(id);
            if (pokemon == null)
            {
                return NotFound();
            }
            return Ok(pokemon);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PokemonCreateDto model)
        {
            var createdPokemon = await _pokemonService.CreateAsync(model);
            return CreatedAtAction(nameof(GetById), new { id = createdPokemon.Id }, createdPokemon);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] PokemonUpdateDto model)
        {
            var updatedPokemon = await _pokemonService.UpdateAsync(id, model);
            if (updatedPokemon == null)
            {
                return NotFound();
            }
            return Ok(updatedPokemon);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _pokemonService.DeleteAsync(id);
            if (!deleted)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
