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
    public class OrderControllerTests : IClassFixture<TestWebApplicationFactory>
    {
        private readonly HttpClient _client;
        private readonly HttpClientHelper _httpClientHelper;

        public OrderControllerTests(TestWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
            _client.Timeout = TimeSpan.FromSeconds(30);
            _client.BaseAddress = new Uri("http://localhost:60021");
            _httpClientHelper = new HttpClientHelper(_client);
        }

        [Fact]
        [Order(1)]
        public async Task TestGetAsync()
        {
            var result = await _httpClientHelper.GetAsync<List<Order>>("/api/order").ConfigureAwait(false);
            Assert.NotNull(result);
        }

        [Fact]
        [Order(2)]
        public async Task TestGetByIdAsync()
        {
            var result = await _httpClientHelper.GetAsync<Order>("/api/order/O1").ConfigureAwait(false);
            Assert.NotNull(result);
        }

        [Fact]
        [Order(3)]
        public async Task TestPostAsync()
        {
            var newOrder = new Order
            {
                Id = "O_TEST",
                DatePlaced = DateTime.Today
            };

            var order = await _httpClientHelper.PostAsync("/api/order", newOrder);
            Assert.Equal("O_TEST", order.Id);
        }

        [Fact]
        [Order(4)]
        public async Task TestDeleteAsync()
        {
            var result = await _httpClientHelper.DeleteAsync("/api/order/O_TEST");
            Assert.True(result == HttpStatusCode.OK);
        }
    }
}
