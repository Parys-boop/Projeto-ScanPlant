using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ScanPlantAPI.Models
{
    /// <summary>
    /// Representa um lembrete do usuário
    /// </summary>
    public class Lembrete
    {
        /// <summary>
        /// Identificador único do lembrete
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Título do lembrete
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Titulo { get; set; } = string.Empty;

        /// <summary>
        /// Descrição detalhada do lembrete
        /// </summary>
        [MaxLength(500)]
        public string Descricao { get; set; } = string.Empty;

        /// <summary>
        /// Data e hora do lembrete
        /// </summary>
        [Required]
        public DateTime DataLembrete { get; set; }

        /// <summary>
        /// Indica se o lembrete foi concluído
        /// </summary>
        public bool Concluido { get; set; } = false;

        /// <summary>
        /// Prioridade do lembrete (1-Baixa, 2-Média, 3-Alta)
        /// </summary>
        [Range(1, 3)]
        public int Prioridade { get; set; } = 2;

        /// <summary>
        /// Categoria do lembrete
        /// </summary>
        [MaxLength(50)]
        public string Categoria { get; set; } = string.Empty;

        /// <summary>
        /// Data e hora em que o lembrete foi criado
        /// </summary>
        public DateTime CriadoEm { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Data e hora da última atualização
        /// </summary>
        public DateTime? AtualizadoEm { get; set; }

        /// <summary>
        /// ID do usuário que criou o lembrete
        /// </summary>
        [Required]
        public string UserId { get; set; } = string.Empty;

        /// <summary>
        /// Referência ao usuário que criou o lembrete
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