using AvDe.Contracts.Models;
using System;

namespace AvDe.Persistence.DbContexts
{
    /// <summary>
    ///     Data seeder for pre-populating the database with default data
    /// </summary>
    public class DbInitializer
    {
        public static void Initialize(AvDeDbContext context)
        {
            // Articles
            Random genPrice = new Random();
            int rangePrices = 100;

            for (int i = 1; i < 101; i++)
            {
                context.Articles.Add(
                new Article
                {
                    Id = $"A{i}",
                    Name = $"Article A{i}",
                    Price = genPrice.Next(1, rangePrices)
                });
            }

            // Orders
            Random genDays = new Random();
            int rangeDays = 10;
            for (int i = 1; i < 101; i++)
            {
                context.Orders.Add(
                new Order
                {
                    Id = $"O{i}",
                    DatePlaced = DateTime.Today.AddDays(-genDays.Next(rangeDays))
                });
            }

            // Order items
            Random genArticles = new Random();
            int rangeArticles = 100;

            Random genOrders = new Random();
            int rangeOrders = 100;

            for (int i = 1; i < 1001; i++)
            {
                context.OrderItems.Add(
                new OrderItem
                {
                    OrderId = $"O{genOrders.Next(1, rangeOrders)}",
                    ArticleId = $"A{genArticles.Next(1, rangeArticles)}",
                    Quantity = genOrders.Next(1, rangeOrders)
                });
            }

            context.SaveChanges();
        }
    }
}
