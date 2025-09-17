using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScanPlantAPI.Models.DTOs;
using ScanPlantAPI.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ScanPlantAPI.Controllers
{
    /// <summary>
    /// Controlador para operações com plantas
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PlantsController : ControllerBase
    {
        private readonly IPlantService _plantService;

        /// <summary>
        /// Construtor do controlador de plantas
        /// </summary>
        /// <param name="plantService">Serviço de plantas</param>
        public PlantsController(IPlantService plantService)
        {
            _plantService = plantService;
        }

        /// <summary>
        /// Obtém todas as plantas
        /// </summary>
        /// <returns>Lista de plantas</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<PlantResponseDTO>), 200)]
        public async Task<IActionResult> GetAllPlants()
        {
            var plants = await _plantService.GetAllPlantsAsync();
            return Ok(plants);
        }

        /// <summary>
        /// Obtém plantas do usuário atual
        /// </summary>
        /// <returns>Lista de plantas do usuário</returns>
        [HttpGet("my-plants")]
        [Authorize]
        [ProducesResponseType(typeof(IEnumerable<PlantResponseDTO>), 200)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> GetMyPlants()
        {
            var userId = User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var plants = await _plantService.GetPlantsByUserIdAsync(userId);
            return Ok(plants);
        }

        /// <summary>
        /// Obtém plantas próximas a uma localização
        /// </summary>
        /// <param name="latitude">Latitude</param>
        /// <param name="longitude">Longitude</param>
        /// <param name="radiusInKm">Raio em quilômetros</param>
        /// <returns>Lista de plantas próximas</returns>
        [HttpGet("nearby")]
        [ProducesResponseType(typeof(IEnumerable<PlantResponseDTO>), 200)]
        public async Task<IActionResult> GetNearbyPlants([FromQuery] double latitude, [FromQuery] double longitude, [FromQuery] double radiusInKm = 5.0)
        {
            var plants = await _plantService.GetNearbyPlantsAsync(latitude, longitude, radiusInKm);
            return Ok(plants);
        }

        /// <summary>
        /// Obtém uma planta pelo ID
        /// </summary>
        /// <param name="id">ID da planta</param>
        /// <returns>Dados da planta</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(PlantResponseDTO), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetPlantById(int id)
        {
            var plant = await _plantService.GetPlantByIdAsync(id);
            if (plant == null)
            {
                return NotFound();
            }

            return Ok(plant);
        }

        /// <summary>
        /// Cria uma nova planta
        /// </summary>
        /// <param name="model">Dados da planta</param>
        /// <returns>Dados da planta criada</returns>
        [HttpPost]
        [Authorize]
        [ProducesResponseType(typeof(PlantResponseDTO), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> CreatePlant([FromForm] CreatePlantDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var plant = await _plantService.CreatePlantAsync(model, userId);
            return CreatedAtAction(nameof(GetPlantById), new { id = plant.Id }, plant);
        }

        /// <summary>
        /// Atualiza uma planta existente
        /// </summary>
        /// <param name="id">ID da planta</param>
        /// <param name="model">Novos dados da planta</param>
        /// <returns>Dados da planta atualizada</returns>
        [HttpPut("{id}")]
        [Authorize]
        [ProducesResponseType(typeof(PlantResponseDTO), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdatePlant(int id, [FromForm] UpdatePlantDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var plant = await _plantService.UpdatePlantAsync(id, model, userId);
            if (plant == null)
            {
                return NotFound();
            }

            return Ok(plant);
        }

        /// <summary>
        /// Exclui uma planta
        /// </summary>
        /// <param name="id">ID da planta</param>
        /// <returns>Mensagem de sucesso</returns>
        [HttpDelete("{id}")]
        [Authorize]
        [ProducesResponseType(204)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeletePlant(int id)
        {
            var userId = User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var result = await _plantService.DeletePlantAsync(id, userId);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        /// <summary>
        /// Busca plantas por nome científico ou comum
        /// </summary>
        /// <param name="searchTerm">Termo de busca</param>
        /// <returns>Lista de plantas encontradas</returns>
        [HttpGet("search")]
        [ProducesResponseType(typeof(IEnumerable<PlantResponseDTO>), 200)]
        public async Task<IActionResult> SearchPlants([FromQuery] string searchTerm)
        {
            var plants = await _plantService.SearchPlantsByNameAsync(searchTerm);
            return Ok(plants);
        }
    }
}