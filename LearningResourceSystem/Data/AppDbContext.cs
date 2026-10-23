
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace LearningResourceSystem.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(
            DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<LearningResourceModel> LearningResources
            => Set<LearningResourceModel>();

        protected override void OnModelCreating(
            ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<LearningResourceModel>(entity =>
            {
                entity.ToTable("LearningResources");

                entity.HasKey(resource => resource.ResourceId);

                entity.Property(resource => resource.Title)
                    .HasMaxLength(200)
                    .IsRequired();

                entity.Property(resource => resource.Topic)
                    .HasMaxLength(100)
                    .IsRequired();

                entity.Property(resource => resource.Description)
                    .IsRequired();

                entity.Property(resource => resource.AuthorName)
                    .HasMaxLength(150)
                    .IsRequired();
            });
        }
    }
}
