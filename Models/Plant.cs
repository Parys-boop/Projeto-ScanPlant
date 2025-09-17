using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ScanPlantAPI.Models
{
    /// <summary>
    /// Representa uma planta identificada pelo usuário
    /// </summary>
    public class Plant
    {
        /// <summary>
        /// Identificador único da planta
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// URL da imagem da planta
        /// </summary>
        [Required]
        public string ImageUrl { get; set; }

        /// <summary>
        /// Nome científico da planta
        /// </summary>
        [Required]
        public string ScientificName { get; set; }

        /// <summary>
        /// Nome comum da planta
        /// </summary>
        public string CommonName { get; set; }

        /// <summary>
        /// Descrição da planta obtida da Wikipedia
        /// </summary>
        public string WikiDescription { get; set; }

        /// <summary>
        /// URL da página da Wikipedia sobre a planta
        /// </summary>
        public string WikiUrl { get; set; }

        /// <summary>
        /// Família botânica da planta
        /// </summary>
        public string Family { get; set; }

        /// <summary>
        /// Gênero botânico da planta
        /// </summary>
        public string Genus { get; set; }

        /// <summary>
        /// Instruções de cuidado para a planta
        /// </summary>
        public string CareInstructions { get; set; }

        /// <summary>
        /// Descrição detalhada da planta
        /// </summary>
        public string EnhancedDescription { get; set; }

        /// <summary>
        /// Latitude onde a planta foi encontrada
        /// </summary>
        public double Latitude { get; set; }

        /// <summary>
        /// Longitude onde a planta foi encontrada
        /// </summary>
        public double Longitude { get; set; }

        /// <summary>
        /// Nome do local onde a planta foi encontrada
        /// </summary>
        public string LocationName { get; set; }

        /// <summary>
        /// Nome da cidade onde a planta foi encontrada
        /// </summary>
        public string CityName { get; set; }

        /// <summary>
        /// Data e hora em que o registro foi criado
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// ID do usuário que registrou a planta
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Referência ao usuário que registrou a planta
        /// </summary>
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }
}