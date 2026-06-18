using CW21Ta23.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CW21Ta23.Infrastructure.Data.Configurations;

public class OrderModelBuilderConfiguration : BaseModelBuilderConfiguration<Order>
{
    protected override void ApplyEntityConfiguration(EntityTypeBuilder<Order> modelBuilder)
    {
        modelBuilder.HasMany(o => o.OrderItems)
            .WithOne(o => o.Order)
            .HasForeignKey(o => o.OrderId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}