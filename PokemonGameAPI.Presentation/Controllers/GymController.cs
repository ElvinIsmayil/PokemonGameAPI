using Microsoft.AspNetCore.Mvc;
using PokemonGameAPI.Contracts.DTOs.Gym;
using PokemonGameAPI.Contracts.Services;

namespace PokemonGameAPI.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GymController : ControllerBase
    {
        private readonly IGymService _gymService;

        public GymController(IGymService gymService)
        {
            _gymService = gymService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(int pageNumber = 1, int pageSize = 10)
        {
            var gyms = await _gymService.GetAllAsync(pageNumber, pageSize);
            return Ok(gyms);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] GymCreateDto model)
        {
            var createdGym = await _gymService.CreateAsync(model);
            return CreatedAtAction(nameof(GetById), new { id = createdGym.Id }, createdGym);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var gym = await _gymService.GetByIdAsync(id);
            if (gym == null)
            {
                return NotFound();
            }
            return Ok(gym);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] GymUpdateDto model)
        {
            var updatedGym = await _gymService.UpdateAsync(id, model);
            if (updatedGym == null)
            {
                return NotFound();
            }
            return Ok(updatedGym);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _gymService.DeleteAsync(id);
            if (!deleted)
            {
                return NotFound();
            }
            return NoContent();
        }


    }
}
