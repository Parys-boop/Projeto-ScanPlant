using Microsoft.EntityFrameworkCore;
using ScanPlantAPI.Data;
using ScanPlantAPI.Models;
using ScanPlantAPI.Models.DTOs;
using ScanPlantAPI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScanPlantAPI.Services
{
    /// <summary>
    /// Implementação do serviço de comentários
    /// </summary>
    public class CommentService : ICommentService
    {
        private readonly ApplicationDbContext _context;

        public CommentService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CommentResponseDTO>> GetCommentsByPlantIdAsync(int plantId)
        {
            var comments = await _context.Comments
                .Include(c => c.User)
                .Include(c => c.Plant)
                .Where(c => c.PlantId == plantId)
                .OrderByDescending(c => c.CreatedAt)
                .ToListAsync();

            return comments.Select(MapToResponseDTO);
        }

        public async Task<CommentResponseDTO?> GetCommentByIdAsync(int id)
        {
            var comment = await _context.Comments
                .Include(c => c.User)
                .Include(c => c.Plant)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (comment == null)
                return null;

            return MapToResponseDTO(comment);
        }

        public async Task<IEnumerable<CommentResponseDTO>> GetCommentsByUserIdAsync(string userId)
        {
            var comments = await _context.Comments
                .Include(c => c.User)
                .Include(c => c.Plant)
                .Where(c => c.UserId == userId)
                .OrderByDescending(c => c.CreatedAt)
                .ToListAsync();

            return comments.Select(MapToResponseDTO);
        }

        public async Task<CommentResponseDTO> CreateCommentAsync(CreateCommentDTO model, string userId)
        {
            // Verifica se a planta existe
            var plantExists = await _context.Plants.AnyAsync(p => p.Id == model.PlantId);
            if (!plantExists)
                throw new ArgumentException("Planta não encontrada");

            var comment = new Comment
            {
                Text = model.Text,
                PlantId = model.PlantId,
                UserId = userId,
                CreatedAt = DateTime.UtcNow
            };

            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();

            // Recarrega o comentário com dados relacionados
            var createdComment = await _context.Comments
                .Include(c => c.User)
                .Include(c => c.Plant)
                .FirstOrDefaultAsync(c => c.Id == comment.Id);

            return MapToResponseDTO(createdComment!);
        }

        public async Task<CommentResponseDTO?> UpdateCommentAsync(int id, UpdateCommentDTO model, string userId)
        {
            var comment = await _context.Comments.FindAsync(id);
            if (comment == null || comment.UserId != userId)
                return null;

            comment.Text = model.Text;
            comment.UpdatedAt = DateTime.UtcNow;

            _context.Comments.Update(comment);
            await _context.SaveChangesAsync();

            // Recarrega o comentário com dados relacionados
            var updatedComment = await _context.Comments
                .Include(c => c.User)
                .Include(c => c.Plant)
                .FirstOrDefaultAsync(c => c.Id == comment.Id);

            return MapToResponseDTO(updatedComment!);
        }

        public async Task<bool> DeleteCommentAsync(int id, string userId)
        {
            var comment = await _context.Comments.FindAsync(id);
            if (comment == null || comment.UserId != userId)
                return false;

            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();

            return true;
        }

        private CommentResponseDTO MapToResponseDTO(Comment comment)
        {
            return new CommentResponseDTO
            {
                Id = comment.Id,
                Text = comment.Text,
                CreatedAt = comment.CreatedAt,
                UpdatedAt = comment.UpdatedAt,
                PlantId = comment.PlantId,
                PlantScientificName = comment.Plant?.ScientificName ?? string.Empty,
                UserId = comment.UserId,
                UserName = comment.User?.UserName ?? string.Empty
            };
        }
    }
}