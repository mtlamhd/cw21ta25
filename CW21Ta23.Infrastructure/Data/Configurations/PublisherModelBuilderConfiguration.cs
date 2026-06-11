using CW21Ta23.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CW21Ta23.Infrastructure.Data.Configurations;

public class PublisherModelBuilderConfiguration : BaseModelBuilderConfiguration<Publisher>
{
    protected override void ApplyEntityConfiguration(EntityTypeBuilder<Publisher> modelBuilder)
    {
       
        modelBuilder.HasIndex(p => p.Name)
            .IsUnique();

        
        modelBuilder.Property(p => p.CreatedAt)
            .HasDefaultValueSql("GETUTCDATE()");

        
        modelBuilder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(200);
        modelBuilder.HasData(SeedData.SeedData.Publishers);
    }
}