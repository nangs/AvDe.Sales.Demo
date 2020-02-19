using AvDe.Contracts.Models;
using AvDe.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AvDe.Persistence.Repositories
{
    /// <summary>
    /// Contains methods for interacting with the orders
    /// </summary>
    public class OrderRepository : IOrderRepository
    {
        private readonly AvDeDbContext _context;

        public OrderRepository(AvDeDbContext context) => _context = context;

        public async Task<IEnumerable<Order>> GetAsync() =>
            await _context.Orders
                .Include(order => order.OrderItems)
                .ThenInclude(orderLine => orderLine.Article)
                .AsNoTracking()
                .ToListAsync();

        public async Task<Order> GetAsync(string id) =>
            await _context.Orders
                .Include(order => order.OrderItems)
                .ThenInclude(orderLine => orderLine.Article)
                .AsNoTracking()
                .FirstOrDefaultAsync(order => order.Id == id);

        public async Task<Order> UpsertAsync(Order order)
        {
            var existing = await _context.Orders.FirstOrDefaultAsync(_order => _order.Id == order.Id);
            if (existing == null)
            {
                _context.Orders.Add(order);
            }
            else
            {
                _context.Entry(existing).CurrentValues.SetValues(order);
            }
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<int> DeleteAsync(string id)
        {
            var match = await _context.Orders.FindAsync(id);
            if (match != null)
            {
                _context.Orders.Remove(match);
            }
            return await _context.SaveChangesAsync();
        }
    }
}
