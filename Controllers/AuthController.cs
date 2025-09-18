using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScanPlantAPI.Models.DTOs;
using ScanPlantAPI.Services.Interfaces;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ScanPlantAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _userService.RegisterAsync(model);
            if (result == null)
                return BadRequest("Falha ao registrar usuário. O email já pode estar em uso ou a senha não atende aos requisitos.");

            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _userService.LoginAsync(model);
            if (result == null)
                return Unauthorized("Email ou senha inválidos.");

            return Ok(result);
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> RequestPasswordReset([FromBody] ResetPasswordRequestDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _userService.RequestPasswordResetAsync(model);
            if (!result)
                return NotFound("Usuário não encontrado.");

            return Ok("Instruções de redefinição de senha foram enviadas para o seu email.");
        }

        [HttpGet("me")]
        [Authorize]
        public async Task<IActionResult> GetCurrentUser()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var user = await _userService.GetCurrentUserAsync(userId);
            if (user == null)
                return Unauthorized();

            return Ok(user);
        }
    }
}
