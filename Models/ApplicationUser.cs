using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ScanPlantAPI.Models
{
    /// <summary>
    /// Representa um usuário do aplicativo
    /// </summary>
    public class ApplicationUser : IdentityUser
    {
        /// <summary>
        /// Nome completo do usuário
        /// </summary>
        [StringLength(100)]
        public string FullName { get; set; }

        /// <summary>
        /// Data de criação da conta
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Data da última atualização da conta
        /// </summary>
        public DateTime? LastUpdated { get; set; }

        /// <summary>
        /// Coleção de plantas registradas pelo usuário
        /// </summary>
        public virtual ICollection<Plant> Plants { get; set; } = new List<Plant>();
        public virtual ICollection<Comment> Comments { get; set; }
    }
}