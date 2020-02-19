using AvDe.Contracts.Models;
using AvDe.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace AvDe.Persistence.DbContexts
{
   public class AvDeDbContext : DbContext
    {
        public AvDeDbContext(DbContextOptions<AvDeDbContext> options) 
            : base(options)
        {
        }

        /// <summary>
        /// Gets the articles DbSet
        /// </summary>
        public DbSet<Article> Articles { get; set; }

        /// <summary>
        /// Gets the orders DbSet
        /// </summary>
        public DbSet<Order> Orders { get; set; }

        /// <summary>
        /// Gets the order lines DbSet
        /// </summary>
        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            new ArticleConfiguration(modelBuilder.Entity<Article>());
            new OrderConfiguration(modelBuilder.Entity<Order>());
            new OrderItemConfiguration(modelBuilder.Entity<OrderItem>());
        }
    }
}
