using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ScanPlantAPI.Models
{
    /// <summary>
    /// Representa um comentário em uma planta
    /// </summary>
    public class Comment
    {
        /// <summary>
        /// Identificador único do comentário
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Texto do comentário
        /// </summary>
        [Required]
        [MaxLength(500)]
        public string Text { get; set; } = string.Empty;

        /// <summary>
        /// Data e hora em que o comentário foi criado
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Data e hora da última atualização do comentário
        /// </summary>
        public DateTime? UpdatedAt { get; set; }

        /// <summary>
        /// ID da planta que recebeu o comentário
        /// </summary>
        public int PlantId { get; set; }

        /// <summary>
        /// Referência à planta que recebeu o comentário
        /// </summary>
        [ForeignKey("PlantId")]
        public Plant? Plant { get; set; }

        /// <summary>
        /// ID do usuário que fez o comentário
        /// </summary>
        public string UserId { get; set; } = string.Empty;

        /// <summary>
        /// Referência ao usuário que fez o comentário
        /// </summary>
        [ForeignKey("UserId")]
        public ApplicationUser? User { get; set; }
    }
}