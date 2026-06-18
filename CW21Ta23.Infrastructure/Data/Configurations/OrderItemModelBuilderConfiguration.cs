using CW21Ta23.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CW21Ta23.Infrastructure.Data.Configurations;

public class OrderItemModelBuilderConfiguration : BaseModelBuilderConfiguration<OrderItem>
{
    protected override void ApplyEntityConfiguration(EntityTypeBuilder<OrderItem> modelBuilder)
    {
        
    }
}