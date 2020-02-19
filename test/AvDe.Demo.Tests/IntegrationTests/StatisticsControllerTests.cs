using AvDe.Contracts.Models.Statistics;
using AvDe.Demo.Tests.Services;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System;
using Xunit;

namespace AvDe.Demo.Tests.IntegrationTests
{
    [TestCaseOrderer("AvDe.Demo.Tests.Services.XUnitUtilities.CustomTestCaseOrderer", "AvDe.Demo.Tests")]
    public class StatisticsControllerTests : IClassFixture<TestWebApplicationFactory>
    {
        private readonly HttpClient _client;
        private readonly HttpClientHelper _httpClientHelper;
        private readonly TestWebApplicationFactory _factory;

        public StatisticsControllerTests(TestWebApplicationFactory factory)
        {
            _factory = factory;
            _client = _factory.CreateClient();
            _client.BaseAddress = new Uri("http://localhost:60021");
            _client.Timeout = TimeSpan.FromSeconds(30);
            _httpClientHelper = new HttpClientHelper(_client);
        }

        [Fact]
        public async Task TestTop10SoldArticlesAsync()
        {
            var result = await _httpClientHelper.GetAsync<List<SoldArticle>>("/api/statistics/top10soldarticles").ConfigureAwait(false);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task TestTop10OrdersByRevenueAsync()
        {
            var result = await _httpClientHelper.GetAsync<List<RevenuePerOrder>>("/api/statistics/top10ordersbyrevenue").ConfigureAwait(false);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task TestSoldArticlesPerDayAsync()
        {
            var result = await _httpClientHelper.GetAsync<List<SoldArticlesPerDay>>("/api/statistics/soldarticlesperday").ConfigureAwait(false);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task TestRevenuePerDayAsync()
        {
            var result = await _httpClientHelper.GetAsync<List<RevenuePerDay>>("/api/statistics/revenueperday").ConfigureAwait(false);
            Assert.NotNull(result);
        }
    }
}
