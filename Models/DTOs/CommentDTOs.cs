using System;

namespace ScanPlantAPI.Models.DTOs
{
    /// <summary>
    /// DTO para criação de comentário
    /// </summary>
    public class CreateCommentDTO
    {
        /// <summary>
        /// Texto do comentário
        /// </summary>
        public string Text { get; set; } = string.Empty;

        /// <summary>
        /// ID da planta que receberá o comentário
        /// </summary>
        public int PlantId { get; set; }
    }

    /// <summary>
    /// DTO para atualização de comentário
    /// </summary>
    public class UpdateCommentDTO
    {
        /// <summary>
        /// Texto do comentário
        /// </summary>
        public string Text { get; set; } = string.Empty;
    }

    /// <summary>
    /// DTO para resposta de comentário
    /// </summary>
    public class CommentResponseDTO
    {
        /// <summary>
        /// ID do comentário
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Texto do comentário
        /// </summary>
        public string Text { get; set; } = string.Empty;

        /// <summary>
        /// Data de criação
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Data de última atualização
        /// </summary>
        public DateTime? UpdatedAt { get; set; }

        /// <summary>
        /// ID da planta
        /// </summary>
        public int PlantId { get; set; }

        /// <summary>
        /// Nome científico da planta
        /// </summary>
        public string PlantScientificName { get; set; } = string.Empty;

        /// <summary>
        /// ID do usuário
        /// </summary>
        public string UserId { get; set; } = string.Empty;

        /// <summary>
        /// Nome do usuário
        /// </summary>
        public string UserName { get; set; } = string.Empty;
    }
}