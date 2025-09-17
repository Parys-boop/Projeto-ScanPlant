using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScanPlantAPI.Models.DTOs;
using ScanPlantAPI.Services;
using ScanPlantAPI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ScanPlantAPI.Controllers
{
    /// <summary>
    /// Controlador para operações com comentários
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentsController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        /// <summary>
        /// Obtém todos os comentários de uma planta
        /// </summary>
        [HttpGet("plant/{plantId}")]
        [ProducesResponseType(typeof(IEnumerable<CommentResponseDTO>), 200)]
        public async Task<IActionResult> GetCommentsByPlantId(int plantId)
        {
            try
            {
                var comments = await _commentService.GetCommentsByPlantIdAsync(plantId);
                return Ok(comments);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }

        /// <summary>
        /// Obtém um comentário pelo ID
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CommentResponseDTO), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetCommentById(int id)
        {
            try
            {
                var comment = await _commentService.GetCommentByIdAsync(id);
                if (comment == null)
                    return NotFound();

                return Ok(comment);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }

        /// <summary>
        /// Obtém comentários do usuário atual
        /// </summary>
        [HttpGet("my-comments")]
        [Authorize]
        [ProducesResponseType(typeof(IEnumerable<CommentResponseDTO>), 200)]
        public async Task<IActionResult> GetMyComments()
        {
            try
            {
                var userId = User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value;
                if (string.IsNullOrEmpty(userId))
                    return Unauthorized();

                var comments = await _commentService.GetCommentsByUserIdAsync(userId);
                return Ok(comments);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }

        /// <summary>
        /// Cria um novo comentário
        /// </summary>
        [HttpPost]
        [Authorize]
        [ProducesResponseType(typeof(CommentResponseDTO), 201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateComment([FromBody] CreateCommentDTO model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var userId = User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value;
                if (string.IsNullOrEmpty(userId))
                    return Unauthorized();

                var comment = await _commentService.CreateCommentAsync(model, userId);
                return CreatedAtAction(nameof(GetCommentById), new { id = comment.Id }, comment);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }

        /// <summary>
        /// Atualiza um comentário
        /// </summary>
        [HttpPut("{id}")]
        [Authorize]
        [ProducesResponseType(typeof(CommentResponseDTO), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateComment(int id, [FromBody] UpdateCommentDTO model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var userId = User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value;
                if (string.IsNullOrEmpty(userId))
                    return Unauthorized();

                var comment = await _commentService.UpdateCommentAsync(id, model, userId);
                if (comment == null)
                    return NotFound();

                return Ok(comment);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }

        /// <summary>
        /// Exclui um comentário
        /// </summary>
        [HttpDelete("{id}")]
        [Authorize]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteComment(int id)
        {
            try
            {
                var userId = User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value;
                if (string.IsNullOrEmpty(userId))
                    return Unauthorized();

                var result = await _commentService.DeleteCommentAsync(id, userId);
                if (!result)
                    return NotFound();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }
    }
}