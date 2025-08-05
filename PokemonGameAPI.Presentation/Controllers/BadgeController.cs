using Microsoft.AspNetCore.Mvc;
using PokemonGameAPI.Contracts.DTOs.Badge;
using PokemonGameAPI.Contracts.Services;

namespace PokemonGameAPI.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BadgeController : ControllerBase
    {
        private readonly IBadgeService _badgeService;

        public BadgeController(IBadgeService badgeService)
        {
            _badgeService = badgeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(int pageNumber = 1, int pageSize = 10)
        {
            var badges = await _badgeService.GetAllAsync(pageNumber, pageSize);
            return Ok(badges);

        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var badge = await _badgeService.GetByIdAsync(id);
            if (badge == null)
            {
                return NotFound();
            }
            return Ok(badge);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] BadgeRequestDto model)
        {
            var createdBadge = await _badgeService.CreateAsync(model);
            return CreatedAtAction(nameof(GetById), new { id = createdBadge.Id }, createdBadge);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] BadgeRequestDto model)
        {
            var updatedBadge = await _badgeService.UpdateAsync(id, model);
            if (updatedBadge == null)
            {
                return NotFound();
            }
            return Ok(updatedBadge);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _badgeService.DeleteAsync(id);
            if (!deleted)
            {
                return NotFound();
            }
            return NoContent();
        }
        //[HttpPost]
        //[Route("assign")]
        //public IActionResult AssignBadgeToTrainer([FromBody] AssignBadgeDto model)
        //{
        //    if (model == null)
        //    {
        //        return BadRequest("Assign badge data is null.");
        //    }
        //    var result = _badgeService.AssignBadgeToTrainer(model);
        //    if (!result)
        //    {
        //        return NotFound("Trainer or Badge not found.");
        //    }
        //    return Ok("Badge assigned successfully.");
        //}


        [HttpPost("{id}/upload-image")]
        public async Task<IActionResult> UploadImage(int id, IFormFile imageFile)
        {
            var result = await _badgeService.UploadImgAsync(id, imageFile);
            return Ok(result);
        }

    }
}