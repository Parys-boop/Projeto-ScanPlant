using ScanPlantAPI.Models;
using ScanPlantAPI.Models.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ScanPlantAPI.Services.Interfaces
{
    /// <summary>
    /// Interface para o serviço de plantas
    /// </summary>
    public interface IPlantService
    {
        /// <summary>
        /// Obtém todas as plantas
        /// </summary>
        /// <returns>Lista de plantas</returns>
        Task<IEnumerable<PlantResponseDTO>> GetAllPlantsAsync();

        /// <summary>
        /// Obtém plantas por usuário
        /// </summary>
        /// <param name="userId">ID do usuário</param>
        /// <returns>Lista de plantas do usuário</returns>
        Task<IEnumerable<PlantResponseDTO>> GetPlantsByUserIdAsync(string userId);

        /// <summary>
        /// Obtém plantas próximas a uma localização
        /// </summary>
        /// <param name="latitude">Latitude</param>
        /// <param name="longitude">Longitude</param>
        /// <param name="radiusInKm">Raio em quilômetros</param>
        /// <returns>Lista de plantas próximas</returns>
        Task<IEnumerable<PlantResponseDTO>> GetNearbyPlantsAsync(double latitude, double longitude, double radiusInKm);

        /// <summary>
        /// Obtém uma planta pelo ID
        /// </summary>
        /// <param name="id">ID da planta</param>
        /// <returns>Dados da planta</returns>
        Task<PlantResponseDTO> GetPlantByIdAsync(int id);

        /// <summary>
        /// Cria uma nova planta
        /// </summary>
        /// <param name="model">Dados da planta</param>
        /// <param name="userId">ID do usuário</param>
        /// <returns>Dados da planta criada</returns>
        Task<PlantResponseDTO> CreatePlantAsync(CreatePlantDTO model, string userId);

        /// <summary>
        /// Atualiza uma planta existente
        /// </summary>
        /// <param name="id">ID da planta</param>
        /// <param name="model">Novos dados da planta</param>
        /// <param name="userId">ID do usuário</param>
        /// <returns>Dados da planta atualizada</returns>
        Task<PlantResponseDTO> UpdatePlantAsync(int id, UpdatePlantDTO model, string userId);

        /// <summary>
        /// Exclui uma planta
        /// </summary>
        /// <param name="id">ID da planta</param>
        /// <param name="userId">ID do usuário</param>
        /// <returns>Verdadeiro se a planta foi excluída com sucesso</returns>
        Task<bool> DeletePlantAsync(int id, string userId);

        /// <summary>
        /// Busca plantas por nome científico ou comum
        /// </summary>
        /// <param name="searchTerm">Termo de busca</param>
        /// <returns>Lista de plantas encontradas</returns>
        Task<IEnumerable<PlantResponseDTO>> SearchPlantsByNameAsync(string searchTerm);
    }
}