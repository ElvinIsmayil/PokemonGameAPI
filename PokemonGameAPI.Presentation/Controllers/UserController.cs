using Microsoft.AspNetCore.Mvc;
using PokemonGameAPI.Contracts.DTOs.User;
using PokemonGameAPI.Contracts.Services;

namespace PokemonGameAPI.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers(int pageNumber = 1, int pageSize = 10)
        {
            var users = await _userService.GetAllUsers(pageNumber, pageSize);
            return Ok(users);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(string id)
        {
            return Ok(await _userService.GetUserById(id));
        }
        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            await _userService.DeleteUser(userId);
            return StatusCode(StatusCodes.Status204NoContent);
        }

        [HttpPut("{userId}")]
        public async Task<IActionResult> Update(string userId, UserUpdateDto user)
        {
            await _userService.UpdateUserAsync(userId, user);
            return Ok();
        }

        [HttpPost("{id}/upload-image")]
        public async Task<IActionResult> UploadImage(string id, IFormFile imageFile)
        {
            var result = await _userService.UploadImgAsync(id, imageFile);
            return Ok(result);
        }
    }
}
