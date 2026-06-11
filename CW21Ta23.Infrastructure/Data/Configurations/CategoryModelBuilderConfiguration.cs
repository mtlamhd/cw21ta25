using CW21Ta23.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CW21Ta23.Infrastructure.Data.Configurations;

public class CategoryModelBuilderConfiguration : BaseModelBuilderConfiguration<Category>
{
    protected override void ApplyEntityConfiguration(EntityTypeBuilder<Category> modelBuilder)
    {
        modelBuilder.Property(u => u.Title)
            .IsRequired();
        
        modelBuilder.HasIndex(u => u.Title)
            .IsUnique();



        modelBuilder.Property(u => u.Description)
            .HasColumnType("nvarchar(400)")
            .IsRequired();

        modelBuilder.HasData(SeedData.SeedData.Categories);
    }
}