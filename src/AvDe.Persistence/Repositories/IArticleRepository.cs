using AvDe.Contracts.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AvDe.Persistence.Repositories
{
    /// <summary>
    /// Defines methods for interacting with the articles backend
    /// </summary>
    public interface IArticleRepository
    {
        /// <summary>
        /// Deletes an article
        /// </summary>
        Task<int> DeleteAsync(string id);

        /// <summary>
        /// Returns all articles
        /// </summary>
        Task<IEnumerable<Article>> GetAsync();

        /// <summary>
        /// Returns the article with the given Id
        /// </summary>
        Task<Article> GetAsync(string id);

        /// <summary>
        /// Returns all articles with a data field matching the start of the given string
        /// </summary>
        Task<IEnumerable<Article>> SearchAsync(string search);

        /// <summary>
        /// Adds a new article if the article does not exist, updates the existing one
        /// </summary>
        Task<Article> UpsertAsync(Article article);
    }
}
