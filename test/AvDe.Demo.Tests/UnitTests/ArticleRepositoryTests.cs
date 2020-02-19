using AvDe.Contracts.Models;
using AvDe.Demo.Tests.Services.XUnitUtilities;
using AvDe.Demo.Tests.Services;
using AvDe.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace AvDe.Demo.Tests.UnitTests
{
    [TestCaseOrderer("AvDe.Demo.Tests.Services.XUnitUtilities.CustomTestCaseOrderer", "AvDe.Demo.Tests")]
    public class ArticleRepositoryTests : IClassFixture<TestWebApplicationFactory>
    {
        private readonly IArticleRepository _articleRepository;

        public ArticleRepositoryTests(TestWebApplicationFactory factory)
        {
            factory.CreateClient();
            _articleRepository = factory.Server.Host.Services.GetRequiredService<IArticleRepository>();
        }

        [Fact]
        [Order(1)]
        public async Task TestUpsertAsync()
        {
            var article = new Article
            {
                Id = "A_TEST_REPO",
                Name = "Article TEST REPOSITORY",
                Price = 999
            };
            var newArticle = await _articleRepository.UpsertAsync(article).ConfigureAwait(false);
            Assert.Equal("A_TEST_REPO", newArticle.Id);
        }

        [Fact]
        [Order(2)]
        public async Task TestGetByIdAsync()
        {
            var result = await _articleRepository.GetAsync("A_TEST_REPO").ConfigureAwait(false) as Article;
            Assert.NotNull(result);
        }

        [Fact]
        [Order(3)]
        public async Task TestGetAsync()
        {
            var result = await _articleRepository.GetAsync().ConfigureAwait(false) as List<Article>;
            Assert.NotNull(result);
        }

        [Fact]
        [Order(4)]
        public async Task TestDeleteAsync()
        {
            var result = await _articleRepository.DeleteAsync("A_TEST_REPO").ConfigureAwait(false);
            Assert.True(result > 0);
        }
    }
}
