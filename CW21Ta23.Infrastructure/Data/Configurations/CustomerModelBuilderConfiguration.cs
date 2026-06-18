using CW21Ta23.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CW21Ta23.Infrastructure.Data.Configurations;

public class CustomerModelBuilderConfiguration : BaseModelBuilderConfiguration<Customer>
{
    protected override void ApplyEntityConfiguration(EntityTypeBuilder<Customer> modelBuilder)
    {
        modelBuilder.Property(c => c.FullName)
            .HasMaxLength(150)
            .IsRequired();
        modelBuilder.Property(c => c.Email)
            .HasMaxLength(150);
        modelBuilder.HasIndex(c => c.Email)
            .IsUnique();
        modelBuilder.Property(c => c.PhoneNumber)
            .HasMaxLength(20);
        modelBuilder.HasMany(c => c.Orders)
            .WithOne(o => o.Customer)
            .HasForeignKey(o => o.CustomerId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}