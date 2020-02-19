using System.ComponentModel.DataAnnotations.Schema;

namespace AvDe.Contracts.Models
{
    /// <summary>
    /// Represents a Order item (article + quantity) on an order
    /// </summary>
    public class OrderItem : DbObject
    {
        /// <summary>
        /// Gets or sets the id of the order the item item is associated with
        /// </summary>
        public string OrderId { get; set; }

        /// <summary>
        /// Gets or sets the order the order item is associated with
        /// </summary>
        [ForeignKey(nameof(OrderId))]
        public Order Order { get; set; }

        /// <summary>
        /// Gets or sets the article's id
        /// </summary>
        public string ArticleId { get; set; }

        /// <summary>
        /// Gets or sets the article
        /// </summary>
        [ForeignKey(nameof(ArticleId))]
        public Article Article { get; set; }

        /// <summary>
        /// Gets or sets the quantity of article
        /// </summary>
        public int Quantity { get; set; } = 1;

        /// <summary>
        /// Gets the order item total amount
        /// </summary>
        public decimal TotalAmount => Article.Price * Quantity;
    }
}