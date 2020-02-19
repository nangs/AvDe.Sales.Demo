using System;
using System.Collections.Generic;
using System.Linq;

namespace AvDe.Contracts.Models
{
    /// <summary>
    /// Represents a order
    /// </summary>
    public class Order : DbObject
    {
        /// <summary>
        /// Gets or sets the lines in the order
        /// </summary>
        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

        /// <summary>
        /// Gets or sets when the order was placed
        /// </summary>
        public DateTime DatePlaced { get; set; } = DateTime.Now;

        /// <summary>
        /// Gets the order's subtotal amount
        /// </summary>
        public decimal SubTotalAmount => OrderItems.Sum(orderline => orderline.TotalAmount);

        /// <summary>
        /// Gets the order's subtotal quantity
        /// </summary>
        public decimal SubTotalQuantity => OrderItems.Sum(orderline => orderline.Quantity);
    }
}
