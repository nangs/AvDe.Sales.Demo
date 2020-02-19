using AvDe.Contracts.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AvDe.Persistence.Repositories
{
    /// <summary>
    /// Defines methods for interacting with the orders backend
    /// </summary>
    public interface IOrderRepository
    {
        /// <summary>
        /// Deletes an order
        /// </summary>
        Task<int> DeleteAsync(string id);

        /// <summary>
        /// Returns all orders
        /// </summary>
        Task<IEnumerable<Order>> GetAsync();

        /// <summary>
        /// Returns the order with the given id
        /// </summary>
        Task<Order> GetAsync(string id);

        /// <summary>
        /// Adds a new order if the order does not exist, updates the existing one
        /// </summary>
        Task<Order> UpsertAsync(Order order);
    }
}
