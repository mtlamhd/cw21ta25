using CW21Ta23.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CW21Ta23.Infrastructure.Data.Configurations;

public class AuthorModelBuilderConfiguration : BaseModelBuilderConfiguration<Author>
{
    protected override void ApplyEntityConfiguration(EntityTypeBuilder<Author> modelBuilder)
    {
        modelBuilder.Property(u => u.FullName)
            .IsRequired();
        modelBuilder.Property(u => u.BirthDate)
            .IsRequired();
        modelBuilder.Property(u => u.Country)
            .IsRequired();
        modelBuilder.HasData(SeedData.SeedData.CreateAuthors);
    }
}