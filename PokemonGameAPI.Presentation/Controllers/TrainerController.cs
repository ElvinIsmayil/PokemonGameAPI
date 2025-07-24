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
    }
}
