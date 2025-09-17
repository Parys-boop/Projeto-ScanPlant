using ScanPlantAPI.Models.DTOs;
using ScanPlantAPI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


//CRIADO TEMPORARIAMENTE POR ARTHUR PARA TESTAR O CRUD

namespace ScanPlantAPI.Services
{
    public class InMemoryCommentService : ICommentService
    {
        private readonly List<CommentResponseDTO> _comments = new();
        private int _nextId = 1;

        public Task<IEnumerable<CommentResponseDTO>> GetCommentsByPlantIdAsync(int plantId)
        {
            var result = _comments.Where(c => c.PlantId == plantId);
            return Task.FromResult(result);
        }

        public Task<CommentResponseDTO?> GetCommentByIdAsync(int id)
        {
            var result = _comments.FirstOrDefault(c => c.Id == id);
            return Task.FromResult(result);
        }

        public Task<IEnumerable<CommentResponseDTO>> GetCommentsByUserIdAsync(string userId)
        {
            var result = _comments.Where(c => c.UserId == userId);
            return Task.FromResult(result);
        }

        public Task<CommentResponseDTO> CreateCommentAsync(CreateCommentDTO model, string userId)
        {
            var comment = new CommentResponseDTO
            {
                Id = _nextId++,
                Text = model.Text,
                PlantId = model.PlantId,
                UserId = userId,
                UserName = "TestUser",
                PlantScientificName = "TestPlant",
                CreatedAt = DateTime.UtcNow
            };

            _comments.Add(comment);
            return Task.FromResult(comment);
        }

        public Task<CommentResponseDTO?> UpdateCommentAsync(int id, UpdateCommentDTO model, string userId)
        {
            var comment = _comments.FirstOrDefault(c => c.Id == id && c.UserId == userId);
            if (comment != null)
            {
                comment.Text = model.Text;
                comment.UpdatedAt = DateTime.UtcNow;
            }
            return Task.FromResult(comment);
        }

        public Task<bool> DeleteCommentAsync(int id, string userId)
        {
            var comment = _comments.FirstOrDefault(c => c.Id == id && c.UserId == userId);
            if (comment != null)
            {
                _comments.Remove(comment);
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }
    }
}