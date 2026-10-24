using Microsoft.EntityFrameworkCore;
using LearningResourceSystem;

namespace LearningResourceSystem.Data
{
    public class ResourcesDbContext : DbContext
    {
        public ResourcesDbContext(
            DbContextOptions<ResourcesDbContext> options)
            : base(options)
        {
        }

        public DbSet<LearningResourceModel> LearningResources =>
            Set<LearningResourceModel>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var resource = modelBuilder.Entity<LearningResourceModel>();

            resource.ToTable("LearningResources", "dbo");

            resource.HasKey(r => r.ResourceId);

            resource.Property(r => r.ResourceId)
                .UseIdentityColumn();

            resource.Property(r => r.Title)
                .HasMaxLength(255)
                .IsRequired();

            resource.Property(r => r.Topic)
                .HasMaxLength(255)
                .IsRequired();

            resource.Property(r => r.Description)
                .HasColumnType("nvarchar(max)")
                .IsRequired();

            resource.Property(r => r.AuthorName)
                .HasMaxLength(255)
                .IsRequired();

            resource.Property(r => r.CreatedDate)
                .HasColumnType("datetime2");
        }
    }      
}
