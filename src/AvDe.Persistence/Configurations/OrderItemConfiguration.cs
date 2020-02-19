using AvDe.Contracts.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AvDe.Persistence.Configurations
{
    public class OrderItemConfiguration
    {
        public OrderItemConfiguration(EntityTypeBuilder<OrderItem> entity)
        {
            // Table
            entity.ToTable("OrderItems");

            // Keys
            entity.HasKey(oi => new { oi.Id, oi.OrderId });
        }
    }
}
