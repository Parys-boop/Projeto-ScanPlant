using ScanPlantAPI.Models.DTOs;
using System.Threading.Tasks;

namespace ScanPlantAPI.Services.Interfaces
{
    public interface IUserService
    {
        Task<AuthResponseDTO?> RegisterAsync(RegisterDTO model);
        Task<AuthResponseDTO?> LoginAsync(LoginDTO model);
        Task<bool> RequestPasswordResetAsync(ResetPasswordRequestDTO model);
        Task<AuthResponseDTO?> GetCurrentUserAsync(string userId);
    }
}
