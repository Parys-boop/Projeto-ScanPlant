using ScanPlantAPI.Models.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ScanPlantAPI.Services.Interfaces
{
    /// <summary>
    /// Interface para o serviço de comentários
    /// </summary>
    public interface ICommentService
    {
        /// <summary>
        /// Obtém todos os comentários de uma planta
        /// </summary>
        Task<IEnumerable<CommentResponseDTO>> GetCommentsByPlantIdAsync(int plantId);

        /// <summary>
        /// Obtém um comentário pelo ID
        /// </summary>
        Task<CommentResponseDTO?> GetCommentByIdAsync(int id);

        /// <summary>
        /// Obtém todos os comentários de um usuário
        /// </summary>
        Task<IEnumerable<CommentResponseDTO>> GetCommentsByUserIdAsync(string userId);

        /// <summary>
        /// Cria um novo comentário
        /// </summary>
        Task<CommentResponseDTO> CreateCommentAsync(CreateCommentDTO model, string userId);

        /// <summary>
        /// Atualiza um comentário existente
        /// </summary>
        Task<CommentResponseDTO?> UpdateCommentAsync(int id, UpdateCommentDTO model, string userId);

        /// <summary>
        /// Exclui um comentário
        /// </summary>
        Task<bool> DeleteCommentAsync(int id, string userId);
    }
}