namespace AvDe.Contracts.Models
{
    /// <summary>
    /// Represents a article
    /// </summary>
    public class Article : DbObject
    { 
        /// <summary>
        /// Gets or sets the article's name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the article's price
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Returns the name of the article and the price
        /// </summary>
        public override string ToString() => $"{Name} \n{Price}";
    }
}