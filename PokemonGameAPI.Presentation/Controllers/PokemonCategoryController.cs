using Microsoft.AspNetCore.Mvc;
using PokemonGameAPI.Contracts.DTOs.PokemonCategory;
using PokemonGameAPI.Contracts.Services;

namespace PokemonGameAPI.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PokemonCategoryController : ControllerBase
    {
        private readonly IPokemonCategoryService _pokemonCategoryService;

        public PokemonCategoryController(IPokemonCategoryService pokemonCategoryService)
        {
            _pokemonCategoryService = pokemonCategoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(int pageNumber = 1, int pageSize = 10)
        {
            var response = await _pokemonCategoryService.GetAllAsync(pageNumber, pageSize);
            return Ok(response);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            if (id <= 0)
            {
                return BadRequest("ID must be greater than zero.");
            }
            var category = await _pokemonCategoryService.GetByIdAsync(id);
            if (category == null)
            {
                return NotFound($"Category with ID {id} not found.");
            }
            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PokemonCategoryRequestDto category)
        {
            var createdCategory = await _pokemonCategoryService.CreateAsync(category);
            return CreatedAtAction(nameof(GetById), new { id = createdCategory.Id }, createdCategory);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] PokemonCategoryRequestDto category)
        {
            if (id <= 0)
            {
                return BadRequest("ID must be greater than zero.");
            }
            var updatedCategory = await _pokemonCategoryService.UpdateAsync(id, category);
            return Ok(updatedCategory);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest("ID must be greater than zero.");
            }
            var success = await _pokemonCategoryService.DeleteAsync(id);
            if (!success)
            {
                return NotFound($"Category with ID {id} not found.");
            }
            return NoContent();

        }
    }

}
