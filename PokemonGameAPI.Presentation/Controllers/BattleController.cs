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

        // New: Start a battle by id
        [HttpPost("{id}/start")]
        public async Task<IActionResult> StartBattle(int id)
        {
            try
            {
                var battle = await _battleService.StartBattleAsync(id);
                return Ok(battle);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // New: Finish a battle by id
        [HttpPost("{id}/finish")]
        public async Task<IActionResult> FinishBattle(int id)
        {
            try
            {
                var battle = await _battleService.FinishBattleAsync(id);
                return Ok(battle);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // New: Execute a turn in a battle
        [HttpPost("execute-turn")]
        public async Task<IActionResult> ExecuteTurn([FromBody] BattleTurnDto turn)
        {
            try
            {
                var result = await _battleService.ExecuteTurnAsync(turn);
                return Ok(result);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // New: Get battle result by id
        [HttpGet("{id}/result")]
        public async Task<IActionResult> GetBattleResult(int id)
        {
            try
            {
                var result = await _battleService.GetBattleResultAsync(id);
                return Ok(result);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
    }
}
