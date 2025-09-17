using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ScanPlantAPI.Models;

namespace ScanPlantAPI.Data
{
    /// <summary>
    /// Contexto do banco de dados da aplicação
    /// </summary>
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        /// <summary>
        /// Construtor do contexto
        /// </summary>
        /// <param name="options">Opções de configuração</param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Tabela de plantas
        /// </summary>
        public DbSet<Plant> Plants { get; set; }
        //Tabela dos comentarios PORRA XD
        public DbSet<Comment> Comments { get; set; }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }

        /// <summary>
        /// Configuração do modelo
        /// </summary>
        /// <param name="modelBuilder">Construtor do modelo</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuração da tabela de plantas
            modelBuilder.Entity<Plant>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.ScientificName).IsRequired();
                entity.Property(e => e.ImageUrl).IsRequired();
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
                
                // Relacionamento com usuário
                entity.HasOne(e => e.User)
                      .WithMany(u => u.Plants)
                      .HasForeignKey(e => e.UserId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Text).IsRequired().HasMaxLength(500);
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");

                // Relacionamento com planta
                entity.HasOne(e => e.Plant)
                      .WithMany(p => p.Comments)
                      .HasForeignKey(e => e.PlantId)
                      .OnDelete(DeleteBehavior.Cascade);

                // Relacionamento com usuário
                entity.HasOne(e => e.User)
                      .WithMany(u => u.Comments)
                      .HasForeignKey(e => e.UserId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}