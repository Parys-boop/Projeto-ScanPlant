using ScanPlantAPI.Models.DTOs;
using System.Threading.Tasks;

namespace ScanPlantAPI.Services.Interfaces
{
    /// <summary>
    /// Interface para o serviço de usuários
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Registra um novo usuário
        /// </summary>
        /// <param name="model">Dados do usuário</param>
        /// <returns>Resposta da autenticação</returns>
        Task<AuthResponseDTO> RegisterAsync(RegisterDTO model);

        /// <summary>
        /// Realiza o login de um usuário
        /// </summary>
        /// <param name="model">Credenciais do usuário</param>
        /// <returns>Resposta da autenticação</returns>
        Task<AuthResponseDTO> LoginAsync(LoginDTO model);

        /// <summary>
        /// Solicita a redefinição de senha
        /// </summary>
        /// <param name="model">Email do usuário</param>
        /// <returns>Verdadeiro se a solicitação foi enviada com sucesso</returns>
        Task<bool> RequestPasswordResetAsync(ResetPasswordRequestDTO model);

        /// <summary>
        /// Obtém o usuário atual pelo ID
        /// </summary>
        /// <param name="userId">ID do usuário</param>
        /// <returns>Dados do usuário</returns>
        Task<AuthResponseDTO> GetCurrentUserAsync(string userId);
    }
}