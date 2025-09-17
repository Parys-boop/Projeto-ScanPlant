using System.ComponentModel.DataAnnotations;

namespace ScanPlantAPI.Models.DTOs
{
    /// <summary>
    /// DTO para registro de novo usuário
    /// </summary>
    public class RegisterDTO
    {
        /// <summary>
        /// Email do usuário (será usado como nome de usuário)
        /// </summary>
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        /// <summary>
        /// Senha do usuário
        /// </summary>
        [Required]
        [StringLength(100, MinimumLength = 6)]
        public string Password { get; set; }

        /// <summary>
        /// Confirmação da senha
        /// </summary>
        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        /// <summary>
        /// Nome completo do usuário
        /// </summary>
        [Required]
        [StringLength(100)]
        public string FullName { get; set; }
    }

    /// <summary>
    /// DTO para login de usuário
    /// </summary>
    public class LoginDTO
    {
        /// <summary>
        /// Email do usuário
        /// </summary>
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        /// <summary>
        /// Senha do usuário
        /// </summary>
        [Required]
        public string Password { get; set; }
    }

    /// <summary>
    /// DTO para resposta de autenticação
    /// </summary>
    public class AuthResponseDTO
    {
        /// <summary>
        /// Token JWT
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// Data de expiração do token
        /// </summary>
        public DateTime Expiration { get; set; }

        /// <summary>
        /// ID do usuário
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Email do usuário
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Nome completo do usuário
        /// </summary>
        public string FullName { get; set; }
    }

    /// <summary>
    /// DTO para solicitação de redefinição de senha
    /// </summary>
    public class ResetPasswordRequestDTO
    {
        /// <summary>
        /// Email do usuário
        /// </summary>
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}