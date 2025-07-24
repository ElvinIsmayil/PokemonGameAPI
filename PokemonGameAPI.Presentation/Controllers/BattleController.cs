using Microsoft.AspNetCore.Mvc;
using PokemonGameAPI.Contracts.DTOs.Battle;
using PokemonGameAPI.Contracts.Services;

namespace PokemonGameAPI.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BattleController : ControllerBase
    {
        private readonly IBattleService _battleService;

        public BattleController(IBattleService battleService)
        {
            _battleService = battleService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(int pageNumber = 1, int pageSize = 10)
        {
            var battles = await _battleService.GetAllAsync(pageNumber, pageSize);
            return Ok(battles);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var battle = await _battleService.GetByIdAsync(id);
            if (battle == null)
            {
                return NotFound();
            }
            return Ok(battle);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] BattleCreateDto model)
        {
            var createdBattle = await _battleService.CreateAsync(model);
            return CreatedAtAction(nameof(GetById), new { id = createdBattle.Id }, createdBattle);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] BattleUpdateDto model)
        {
            var updatedBattle = await _battleService.UpdateAsync(id, model);
            if (updatedBattle == null)
            {
                return NotFound();
            }
            return Ok(updatedBattle);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _battleService.DeleteAsync(id);
            if (!deleted)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
