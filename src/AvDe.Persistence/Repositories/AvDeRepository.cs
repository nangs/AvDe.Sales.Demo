using AvDe.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace AvDe.Persistence.Repositories
{
    /// <summary>
    /// Contains methods for interacting with the app backend
    /// </summary>
    public class AvDeRepository : IAvDeRepository
    {
        private readonly DbContextOptions<AvDeDbContext> _dbOptions;

        public AvDeRepository(DbContextOptionsBuilder<AvDeDbContext>dbOptionsBuilder)
        {
            _dbOptions = dbOptionsBuilder.Options;
            var context = new AvDeDbContext(_dbOptions);
            context.Database.EnsureCreated();
        }

        public IOrderRepository Orders => new OrderRepository(new AvDeDbContext(_dbOptions));

        public IArticleRepository Articles => new ArticleRepository(new AvDeDbContext(_dbOptions));
    }
}
