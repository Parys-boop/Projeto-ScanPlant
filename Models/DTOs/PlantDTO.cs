using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace ScanPlantAPI.Models.DTOs
{
    /// <summary>
    /// DTO para criação de uma nova planta
    /// </summary>
    public class CreatePlantDTO
    {
        /// <summary>
        /// Imagem da planta
        /// </summary>
        [Required]
        public IFormFile Image { get; set; }

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
    }

    /// <summary>
    /// DTO para atualização de uma planta existente
    /// </summary>
    public class UpdatePlantDTO
    {
        /// <summary>
        /// Nova imagem da planta (opcional)
        /// </summary>
        public IFormFile Image { get; set; }

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
    }

    /// <summary>
    /// DTO para resposta com dados de uma planta
    /// </summary>
    public class PlantResponseDTO
    {
        /// <summary>
        /// Identificador único da planta
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// URL da imagem da planta
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        /// Nome científico da planta
        /// </summary>
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
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// ID do usuário que registrou a planta
        /// </summary>
        public string UserId { get; set; }
    }
}