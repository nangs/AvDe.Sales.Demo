using AvDe.Contracts.Models;
using AvDe.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AvDe.Persistence.Repositories
{
    /// <summary>
    /// Contains methods for interacting with the articles backend
    /// </summary>
    public class ArticleRepository : IArticleRepository
    {
        private readonly AvDeDbContext _context;

        public ArticleRepository(AvDeDbContext context)
        {
            _context = context; 
        }

        public async Task<IEnumerable<Article>> GetAsync()
        {
            return await _context.Articles
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Article> GetAsync(string id)
        {
            return await _context.Articles
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<IEnumerable<Article>> SearchAsync(string value)
        {
            return await _context.Articles.Where(a =>
                a.Id.StartsWith(value) ||
                a.Name.StartsWith(value) ||
                a.Price.ToString().StartsWith(value))
            .AsNoTracking()
            .ToListAsync();
        }

        public async Task<Article> UpsertAsync(Article article)
        {
            var existing = await _context.Articles.FirstOrDefaultAsync(_article => _article.Id == article.Id);
            if (existing == null)
            {
                _context.Articles.Add(article);
            }
            else
            {
                _context.Entry(existing).CurrentValues.SetValues(article);
            }
            await _context.SaveChangesAsync();
            return article;
        }

        public async Task<int> DeleteAsync(string id)
        {
            var match = await _context.Articles.FindAsync(id);
            if (match != null)
            {
                _context.Articles.Remove(match);
            }
            return await _context.SaveChangesAsync();
        }
    }
}
