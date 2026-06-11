using CW21Ta23.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CW21Ta23.Infrastructure.Data.Configurations;

public class TagModelBuilderConfiguration : BaseModelBuilderConfiguration<Tag>
{
    protected override void ApplyEntityConfiguration(EntityTypeBuilder<Tag> modelBuilder)
    {
        modelBuilder.Property(x => x.Name)
            .HasMaxLength(50)
            .IsRequired();
        modelBuilder.HasIndex(x => x.Name)
            .IsUnique();
        
        modelBuilder.Property(t => t.CreatedAt)
            .HasDefaultValueSql("GETDATE()");
        
        modelBuilder.HasData(SeedData.SeedData.Tags);
    }
}