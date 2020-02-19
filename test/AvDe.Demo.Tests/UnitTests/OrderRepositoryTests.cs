using AvDe.Contracts.Models;
using AvDe.Demo.Tests.Services.XUnitUtilities;
using AvDe.Demo.Tests.Services;
using AvDe.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using System;

namespace AvDe.Demo.Tests.UnitTests
{
    [TestCaseOrderer("AvDe.Demo.Tests.Services.XUnitUtilities.CustomTestCaseOrderer", "AvDe.Demo.Tests")]
    public class OrderRepositoryTests : IClassFixture<TestWebApplicationFactory>
    {
        private readonly IOrderRepository _orderRepository;

        public OrderRepositoryTests(TestWebApplicationFactory factory)
        {
            factory.CreateClient();
            _orderRepository = factory.Server.Host.Services.GetRequiredService<IOrderRepository>();
        }

        [Fact]
        [Order(1)]
        public async Task TestUpsertAsync()
        {
            var order = new Order
            {
                Id = "O_TEST_REPO",
                DatePlaced = DateTime.Today
            };
            var newOrder = await _orderRepository.UpsertAsync(order).ConfigureAwait(false);
            Assert.Equal("O_TEST_REPO", newOrder.Id);
        }

        [Fact]
        [Order(2)]
        public async Task TestGetByIdAsync()
        {
            var result = await _orderRepository.GetAsync("O_TEST_REPO").ConfigureAwait(false) as Order;
            Assert.NotNull(result);
        }

        [Fact]
        [Order(3)]
        public async Task TestGetAsync()
        {
            var result = await _orderRepository.GetAsync().ConfigureAwait(false) as List<Order>;
            Assert.NotNull(result);
        }

        [Fact]
        [Order(4)]
        public async Task TestDeleteAsync()
        {
            var result = await _orderRepository.DeleteAsync("O_TEST_REPO").ConfigureAwait(false);
            Assert.True(result > 0);
        }
    }
}
