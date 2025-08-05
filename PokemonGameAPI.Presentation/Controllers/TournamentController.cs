using Microsoft.AspNetCore.Mvc;
using PokemonGameAPI.Contracts.DTOs.Tournament;
using PokemonGameAPI.Contracts.Services;

namespace PokemonGameAPI.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TournamentController : ControllerBase
    {
        private readonly ITournamentService _tournamentService;

        public TournamentController(ITournamentService tournamentService)
        {
            _tournamentService = tournamentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(int pageNumber = 1, int pageSize = 10)
        {
            var tournaments = await _tournamentService.GetAllAsync(pageNumber, pageSize);
            return Ok(tournaments);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var tournament = await _tournamentService.GetByIdAsync(id);
            if (tournament == null)
            {
                return NotFound();
            }
            return Ok(tournament);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TournamentRequestDto model)
        {
            var createdTournament = await _tournamentService.CreateAsync(model);
            return CreatedAtAction(nameof(GetById), new { id = createdTournament.Id }, createdTournament);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] TournamentRequestDto model)
        {
            var updatedTournament = await _tournamentService.UpdateAsync(id, model);
            if (updatedTournament == null)
            {
                return NotFound();
            }
            return Ok(updatedTournament);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _tournamentService.DeleteAsync(id);
            if (!deleted)
            {
                return NotFound();
            }
            return NoContent();
        }

    }
}
