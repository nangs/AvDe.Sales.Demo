using AvDe.Contracts.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AvDe.Persistence.Configurations
{
	public class OrderConfiguration
	{
		public OrderConfiguration(EntityTypeBuilder<Order> entity)
		{
			// Table
			entity.ToTable("Orders");

			// Keys
			entity.HasKey(e => e.Id);

			// Properties
			entity.Property(e => e.DatePlaced)
				.IsRequired();

			// Relationships
			entity.HasMany(a => a.OrderItems)
				.WithOne(b => b.Order)
				.HasForeignKey(c => c.OrderId)
				.OnDelete(DeleteBehavior.Cascade);
		}
	}
}
