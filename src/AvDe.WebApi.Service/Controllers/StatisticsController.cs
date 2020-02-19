using AvDe.Contracts.Models;
using AvDe.Persistence.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AvDe.WebApi.Service.Controllers
{
    /// <summary>
    /// Contains methods for Sale Statistics
    /// </summary>
    [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class StatisticsController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;

        public StatisticsController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        /// <summary>
        /// Gets Top 10 sold articles by revenue
        /// </summary>
        [HttpGet("top10soldarticles")]
        public async Task<IActionResult> GetTop10Async()
        {
            var allOrders = await _orderRepository.GetAsync().ConfigureAwait(false);
            var allOrderItems = new List<OrderItem>();
            foreach (var order in allOrders)
            {
                allOrderItems.AddRange(order.OrderItems);
            }
            var result = allOrderItems.GroupBy(oi => oi.ArticleId).Select(oii => 
                new { ArticleId = oii.Key, ArticleName = oii.Max(a => a.Article.Name), TotalSalesAmount = oii.Sum(a => a.TotalAmount) })
                .OrderByDescending(order => order.TotalSalesAmount).Take(10);
            return Ok(result);
        }

        /// <summary>
        /// Gets number of sold articles per day for last 7 days
        /// </summary>
        [HttpGet("soldarticlesperday")]
        public async Task<IActionResult> GetSalePerDayAsync()
        {
            var allOrders = await _orderRepository.GetAsync();
            var ordersPerDay = allOrders.GroupBy(order => order.DatePlaced).OrderByDescending(g => g.Key).Take(7);
            var result = ordersPerDay.Select(order => new { Date = order.Key, TotalSalesQuantity = order.Sum(a => a.SubTotalQuantity) }).OrderByDescending(g => g.TotalSalesQuantity);
            return Ok(result);
        }

        /// <summary>
        /// Gets total sale/revenue for last 7 days
        /// </summary>
        [HttpGet("revenueperday")]
        public async Task<IActionResult> GetRevenuePerDayAsync()
        {
            var allOrders = await _orderRepository.GetAsync();
            var ordersPerDay = allOrders.GroupBy(order => order.DatePlaced).OrderByDescending(g => g.Key).Take(7);
            var result = ordersPerDay.Select(order => new { Date = order.Key, TotalSalesAmount = order.Sum(a => a.SubTotalAmount) }).OrderByDescending(g => g.TotalSalesAmount);
            return Ok(result);
        }

        /// <summary>
        /// Gets top 10 orders by revenue
        /// </summary>
        [HttpGet("top10ordersbyrevenue")]
        public async Task<IActionResult> GetTop10Orders()
        {
            var allOrders = await _orderRepository.GetAsync().ConfigureAwait(false);
            var top10Orders = allOrders.OrderByDescending(order => order.SubTotalAmount).Take(10);
            var result = top10Orders.Select(order => new { OrderId = order.Id, order.DatePlaced, TotalSalesAmount = order.SubTotalAmount });
            return Ok(result);
        }
    }
}
