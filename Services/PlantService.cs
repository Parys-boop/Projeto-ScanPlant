using Microsoft.EntityFrameworkCore;
using ScanPlantAPI.Data;
using ScanPlantAPI.Models;
using ScanPlantAPI.Models.DTOs;
using ScanPlantAPI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ScanPlantAPI.Services
{
    /// <summary>
    /// Implementação do serviço de plantas
    /// </summary>
    public class PlantService : IPlantService
    {
        private readonly ApplicationDbContext _context;
        private readonly IStorageService _storageService;

        /// <summary>
        /// Construtor do serviço de plantas
        /// </summary>
        /// <param name="context">Contexto do banco de dados</param>
        /// <param name="storageService">Serviço de armazenamento</param>
        public PlantService(ApplicationDbContext context, IStorageService storageService)
        {
            _context = context;
            _storageService = storageService;
        }

        /// <summary>
        /// Obtém todas as plantas
        /// </summary>
        /// <returns>Lista de plantas</returns>
        public async Task<IEnumerable<PlantResponseDTO>> GetAllPlantsAsync()
        {
            var plants = await _context.Plants
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync();

            return plants.Select(MapToResponseDTO);
        }

        /// <summary>
        /// Obtém plantas por usuário
        /// </summary>
        /// <param name="userId">ID do usuário</param>
        /// <returns>Lista de plantas do usuário</returns>
        public async Task<IEnumerable<PlantResponseDTO>> GetPlantsByUserIdAsync(string userId)
        {
            var plants = await _context.Plants
                .Where(p => p.UserId == userId)
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync();

            return plants.Select(MapToResponseDTO);
        }

        /// <summary>
        /// Obtém plantas próximas a uma localização
        /// </summary>
        /// <param name="latitude">Latitude</param>
        /// <param name="longitude">Longitude</param>
        /// <param name="radiusInKm">Raio em quilômetros</param>
        /// <returns>Lista de plantas próximas</returns>
        public async Task<IEnumerable<PlantResponseDTO>> GetNearbyPlantsAsync(double latitude, double longitude, double radiusInKm)
        {
            // Constante para converter graus para radianos
            const double degToRad = Math.PI / 180.0;
            // Raio da Terra em quilômetros
            const double earthRadius = 6371.0;

            // Converte o raio para graus (aproximadamente)
            double radiusInDegrees = radiusInKm / (earthRadius * degToRad);

            // Obtém todas as plantas do banco de dados
            var allPlants = await _context.Plants.ToListAsync();

            // Filtra as plantas que estão dentro do raio especificado
            var nearbyPlants = allPlants.Where(p =>
            {
                // Calcula a distância usando a fórmula de Haversine
                double dLat = (p.Latitude - latitude) * degToRad;
                double dLon = (p.Longitude - longitude) * degToRad;
                double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                           Math.Cos(latitude * degToRad) * Math.Cos(p.Latitude * degToRad) *
                           Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
                double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
                double distance = earthRadius * c;

                return distance <= radiusInKm;
            });

            return nearbyPlants.Select(MapToResponseDTO);
        }

        /// <summary>
        /// Obtém uma planta pelo ID
        /// </summary>
        /// <param name="id">ID da planta</param>
        /// <returns>Dados da planta</returns>
        public async Task<PlantResponseDTO> GetPlantByIdAsync(int id)
        {
            var plant = await _context.Plants.FindAsync(id);
            if (plant == null)
            {
                return null;
            }

            return MapToResponseDTO(plant);
        }

        /// <summary>
        /// Cria uma nova planta
        /// </summary>
        /// <param name="model">Dados da planta</param>
        /// <param name="userId">ID do usuário</param>
        /// <returns>Dados da planta criada</returns>
        public async Task<PlantResponseDTO> CreatePlantAsync(CreatePlantDTO model, string userId)
        {
            // Faz upload da imagem
            string imageUrl = await _storageService.UploadFileAsync(model.Image);

            // Cria a planta
            var plant = new Plant
            {
                ImageUrl = imageUrl,
                ScientificName = model.ScientificName,
                CommonName = model.CommonName,
                WikiDescription = model.WikiDescription,
                WikiUrl = model.WikiUrl,
                Family = model.Family,
                Genus = model.Genus,
                CareInstructions = model.CareInstructions,
                EnhancedDescription = model.EnhancedDescription,
                Latitude = model.Latitude,
                Longitude = model.Longitude,
                LocationName = model.LocationName,
                CityName = model.CityName,
                CreatedAt = DateTime.UtcNow,
                UserId = userId
            };

            _context.Plants.Add(plant);
            await _context.SaveChangesAsync();

            return MapToResponseDTO(plant);
        }

        /// <summary>
        /// Atualiza uma planta existente
        /// </summary>
        /// <param name="id">ID da planta</param>
        /// <param name="model">Novos dados da planta</param>
        /// <param name="userId">ID do usuário</param>
        /// <returns>Dados da planta atualizada</returns>
        public async Task<PlantResponseDTO> UpdatePlantAsync(int id, UpdatePlantDTO model, string userId)
        {
            var plant = await _context.Plants.FindAsync(id);
            if (plant == null || plant.UserId != userId)
            {
                return null;
            }

            // Atualiza a imagem se fornecida
            if (model.Image != null)
            {
                // Extrai o nome do arquivo da URL atual
                string currentFileName = Path.GetFileName(plant.ImageUrl);
                // Remove a imagem atual
                await _storageService.DeleteFileAsync(currentFileName);
                // Faz upload da nova imagem
                plant.ImageUrl = await _storageService.UploadFileAsync(model.Image);
            }

            // Atualiza os dados da planta
            plant.ScientificName = model.ScientificName;
            plant.CommonName = model.CommonName;
            plant.WikiDescription = model.WikiDescription;
            plant.WikiUrl = model.WikiUrl;
            plant.Family = model.Family;
            plant.Genus = model.Genus;
            plant.CareInstructions = model.CareInstructions;
            plant.EnhancedDescription = model.EnhancedDescription;
            plant.Latitude = model.Latitude;
            plant.Longitude = model.Longitude;
            plant.LocationName = model.LocationName;
            plant.CityName = model.CityName;

            _context.Plants.Update(plant);
            await _context.SaveChangesAsync();

            return MapToResponseDTO(plant);
        }

        /// <summary>
        /// Exclui uma planta
        /// </summary>
        /// <param name="id">ID da planta</param>
        /// <param name="userId">ID do usuário</param>
        /// <returns>Verdadeiro se a planta foi excluída com sucesso</returns>
        public async Task<bool> DeletePlantAsync(int id, string userId)
        {
            var plant = await _context.Plants.FindAsync(id);
            if (plant == null || plant.UserId != userId)
            {
                return false;
            }

            // Extrai o nome do arquivo da URL
            string fileName = Path.GetFileName(plant.ImageUrl);
            // Remove a imagem
            await _storageService.DeleteFileAsync(fileName);

            // Remove a planta
            _context.Plants.Remove(plant);
            await _context.SaveChangesAsync();

            return true;
        }

        /// <summary>
        /// Busca plantas por nome científico ou comum
        /// </summary>
        /// <param name="searchTerm">Termo de busca</param>
        /// <returns>Lista de plantas encontradas</returns>
        public async Task<IEnumerable<PlantResponseDTO>> SearchPlantsByNameAsync(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return await GetAllPlantsAsync();
            }

            searchTerm = searchTerm.ToLower();

            var plants = await _context.Plants
                .Where(p => p.ScientificName.ToLower().Contains(searchTerm) ||
                           p.CommonName.ToLower().Contains(searchTerm))
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync();

            return plants.Select(MapToResponseDTO);
        }

        /// <summary>
        /// Mapeia uma entidade Plant para um DTO de resposta
        /// </summary>
        /// <param name="plant">Entidade Plant</param>
        /// <returns>DTO de resposta</returns>
        private PlantResponseDTO MapToResponseDTO(Plant plant)
        {
            return new PlantResponseDTO
            {
                Id = plant.Id,
                ImageUrl = plant.ImageUrl,
                ScientificName = plant.ScientificName,
                CommonName = plant.CommonName,
                WikiDescription = plant.WikiDescription,
                WikiUrl = plant.WikiUrl,
                Family = plant.Family,
                Genus = plant.Genus,
                CareInstructions = plant.CareInstructions,
                EnhancedDescription = plant.EnhancedDescription,
                Latitude = plant.Latitude,
                Longitude = plant.Longitude,
                LocationName = plant.LocationName,
                CityName = plant.CityName,
                CreatedAt = plant.CreatedAt,
                UserId = plant.UserId
            };
        }
    }
}