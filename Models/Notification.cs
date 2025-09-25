using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ScanPlantAPI.Models
{
    /// <summary>
    /// Representa uma notificação do sistema para um usuário
    /// </summary>
    public class Notification
    {
        /// <summary>
        /// Identificador único da notificação
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Título da notificação
        /// </summary>
        [Required]
        [MaxLength(120)]
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Mensagem detalhada da notificação
        /// </summary>
        [Required]
        [MaxLength(1000)]
        public string Message { get; set; } = string.Empty;

        /// <summary>
        /// Tipo/categoria da notificação (ex.: info, alerta, vencimento_rega)
        /// </summary>
        [MaxLength(60)]
        public string Type { get; set; } = "info";

        /// <summary>
        /// Status da notificação (Pending, Sent, Read)
        /// </summary>
        [MaxLength(20)]
        public string Status { get; set; } = "Pending";

        /// <summary>
        /// Data de criação
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Data de envio (quando disponibilizada ao usuário)
        /// </summary>
        public DateTime? SentAt { get; set; }

        /// <summary>
        /// Data em que o usuário marcou como lida
        /// </summary>
        public DateTime? ReadAt { get; set; }

        /// <summary>
        /// Link/ação (opcional) associado à notificação
        /// </summary>
        [MaxLength(300)]
        public string? LinkUrl { get; set; }

        /// <summary>
        /// ID do usuário destinatário
        /// </summary>
        [Required]
        public string UserId { get; set; } = string.Empty;

        /// <summary>
        /// Referência ao usuário
        /// </summary>
        [ForeignKey("UserId")]
        public ApplicationUser? User { get; set; }

        /// <summary>
        /// ID da planta relacionada (opcional)
        /// </summary>
        public int? PlantId { get; set; }

        /// <summary>
        /// Referência à planta relacionada (opcional)
        /// </summary>
        [ForeignKey("PlantId")]
        public Plant? Plant { get; set; }
    }
}