namespace AvDe.Persistence.Repositories
{
    /// <summary>
    /// Defines methods for interacting with the app backend
    /// </summary>
    public interface IAvDeRepository
    {
        /// <summary>
        /// Returns the orders repository
        /// </summary>
        IOrderRepository Orders { get; }

        /// <summary>
        /// Returns the articles repository
        /// </summary>
        IArticleRepository Articles { get;  }
    }
}
