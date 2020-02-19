using AvDe.Contracts.Models;
using AvDe.Demo.Tests.Services;
using AvDe.Demo.Tests.Services.XUnitUtilities;
using System.Collections.Generic;
using System.Net.Http;
using System.Net;
using System.Threading.Tasks;
using System;
using Xunit;

namespace AvDe.Demo.Tests.IntegrationTests
{
    [TestCaseOrderer("AvDe.Demo.Tests.Services.XUnitUtilities.CustomTestCaseOrderer", "AvDe.Demo.Tests")]
    public class ArticleControllerTests : IClassFixture<TestWebApplicationFactory>
    {
        private readonly HttpClient _client;
        private readonly HttpClientHelper _httpClientHelper;
        private readonly TestWebApplicationFactory _factory;

        public ArticleControllerTests(TestWebApplicationFactory factory)
        {
            _factory = factory;
            _client = _factory.CreateClient();
            _client.BaseAddress = new Uri("http://localhost:60021");
            _client.Timeout = TimeSpan.FromSeconds(30);
            _httpClientHelper = new HttpClientHelper(_client);
        }

        [Fact]
        [Order(1)]
        public async Task TestGetAsync()
        {
            var result = await _httpClientHelper.GetAsync<List<Article>>("/api/article").ConfigureAwait(false);
            Assert.NotNull(result);
        }

        [Fact]
        [Order(2)]
        public async Task TestGetByIdAsync()
        {
            var result = await _httpClientHelper.GetAsync<Order>("/api/article/A1").ConfigureAwait(false);
            Assert.NotNull(result);
        }

        [Fact]
        [Order(3)]
        public async Task TestPostAsync()
        {
            var newArticle = new Article
            {
                Id = "A_TEST",
                Name = "Article TEST",
                Price = 999
            };
            var article = await _httpClientHelper.PostAsync("/api/article", newArticle);
            Assert.Equal("A_TEST", article.Id);
        }

        [Fact]
        [Order(4)]
        public async Task TestDeleteAsync()
        {
            var result = await _httpClientHelper.DeleteAsync("/api/article/A_TEST");
            Assert.True(result == HttpStatusCode.OK);
        }
    }
}
