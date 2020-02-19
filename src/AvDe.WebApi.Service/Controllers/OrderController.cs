using AvDe.Contracts.Models;
using AvDe.Persistence.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AvDe.WebApi.Service.Controllers
{
    /// <summary>
    /// Contains methods for interacting with order data
    /// </summary>
    [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;

        public OrderController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        /// <summary>
        /// Deletes an order with the given id
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _orderRepository.DeleteAsync(id).ConfigureAwait(false);
            return Ok();
        }

        /// <summary>
        /// Gets all orders
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _orderRepository.GetAsync().ConfigureAwait(false);
            return Ok(result);
        }

        /// <summary>
        /// Gets the with the given id
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return BadRequest();
            }
            var result = await _orderRepository.GetAsync(id).ConfigureAwait(false);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        /// <summary>
        /// Creates a new order or updates an existing one
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Order order)
        {
            var result = await _orderRepository.UpsertAsync(order).ConfigureAwait(false);
            return Ok(result);
        }
    }
}
