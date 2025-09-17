using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ScanPlantAPI.Models;
using ScanPlantAPI.Models.DTOs;
using ScanPlantAPI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ScanPlantAPI.Services
{
    /// <summary>
    /// Implementação do serviço de usuários
    /// </summary>
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Construtor do serviço de usuários
        /// </summary>
        /// <param name="userManager">Gerenciador de usuários</param>
        /// <param name="configuration">Configuração da aplicação</param>
        public UserService(UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        /// <summary>
        /// Registra um novo usuário
        /// </summary>
        /// <param name="model">Dados do usuário</param>
        /// <returns>Resposta da autenticação</returns>
        public async Task<AuthResponseDTO> RegisterAsync(RegisterDTO model)
        {
            var userExists = await _userManager.FindByEmailAsync(model.Email);
            if (userExists != null)
            {
                return null; // Usuário já existe
            }

            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                FullName = model.FullName,
                CreatedAt = DateTime.UtcNow
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                return null; // Falha ao criar usuário
            }

            return await GenerateJwtToken(user);
        }

        /// <summary>
        /// Realiza o login de um usuário
        /// </summary>
        /// <param name="model">Credenciais do usuário</param>
        /// <returns>Resposta da autenticação</returns>
        public async Task<AuthResponseDTO> LoginAsync(LoginDTO model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return null; // Usuário não encontrado
            }

            var result = await _userManager.CheckPasswordAsync(user, model.Password);
            if (!result)
            {
                return null; // Senha incorreta
            }

            return await GenerateJwtToken(user);
        }

        /// <summary>
        /// Solicita a redefinição de senha
        /// </summary>
        /// <param name="model">Email do usuário</param>
        /// <returns>Verdadeiro se a solicitação foi enviada com sucesso</returns>
        public async Task<bool> RequestPasswordResetAsync(ResetPasswordRequestDTO model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return false; // Usuário não encontrado
            }

            // Em um cenário real, aqui seria gerado um token e enviado por email
            // Para simplificar, apenas retornamos true
            return true;
        }

        /// <summary>
        /// Obtém o usuário atual pelo ID
        /// </summary>
        /// <param name="userId">ID do usuário</param>
        /// <returns>Dados do usuário</returns>
        public async Task<AuthResponseDTO> GetCurrentUserAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return null; // Usuário não encontrado
            }

            return new AuthResponseDTO
            {
                UserId = user.Id,
                Email = user.Email,
                FullName = user.FullName
            };
        }

        /// <summary>
        /// Gera um token JWT para o usuário
        /// </summary>
        /// <param name="user">Usuário</param>
        /// <returns>Resposta da autenticação com token</returns>
        private async Task<AuthResponseDTO> GenerateJwtToken(ApplicationUser user)
        {
            var userRoles = await _userManager.GetRolesAsync(user);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("FullName", user.FullName ?? string.Empty)
            };

            foreach (var role in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["Jwt:DurationInMinutes"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: expires,
                signingCredentials: creds
            );

            return new AuthResponseDTO
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expires,
                UserId = user.Id,
                Email = user.Email,
                FullName = user.FullName
            };
        }
    }
}