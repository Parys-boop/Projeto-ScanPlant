using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScanPlantAPI.Models.DTOs;
using ScanPlantAPI.Services.Interfaces;
using System.Threading.Tasks;

namespace ScanPlantAPI.Controllers
{
    /// <summary>
    /// Controlador para operações de autenticação
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        /// <summary>
        /// Construtor do controlador de autenticação
        /// </summary>
        /// <param name="userService">Serviço de usuários</param>
        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Registra um novo usuário
        /// </summary>
        /// <param name="model">Dados do usuário</param>
        /// <returns>Dados de autenticação</returns>
        [HttpPost("register")]
        [ProducesResponseType(typeof(AuthResponseDTO), 200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Register([FromBody] RegisterDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _userService.RegisterAsync(model);
            if (result == null)
            {
                return BadRequest("Falha ao registrar usuário. O email já pode estar em uso.");
            }

            return Ok(result);
        }

        /// <summary>
        /// Realiza o login de um usuário
        /// </summary>
        /// <param name="model">Credenciais do usuário</param>
        /// <returns>Dados de autenticação</returns>
        [HttpPost("login")]
        [ProducesResponseType(typeof(AuthResponseDTO), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> Login([FromBody] LoginDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _userService.LoginAsync(model);
            if (result == null)
            {
                return Unauthorized("Email ou senha inválidos.");
            }

            return Ok(result);
        }

        /// <summary>
        /// Solicita a redefinição de senha
        /// </summary>
        /// <param name="model">Email do usuário</param>
        /// <returns>Mensagem de sucesso</returns>
        [HttpPost("reset-password")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> RequestPasswordReset([FromBody] ResetPasswordRequestDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _userService.RequestPasswordResetAsync(model);
            if (!result)
            {
                return NotFound("Usuário não encontrado.");
            }

            return Ok("Instruções de redefinição de senha foram enviadas para o seu email.");
        }

        /// <summary>
        /// Obtém o usuário atual
        /// </summary>
        /// <returns>Dados do usuário</returns>
        [HttpGet("me")]
        [Authorize]
        [ProducesResponseType(typeof(AuthResponseDTO), 200)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> GetCurrentUser()
        {
            var userId = User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var user = await _userService.GetCurrentUserAsync(userId);
            if (user == null)
            {
                return Unauthorized();
            }

            return Ok(user);
        }
    }
}