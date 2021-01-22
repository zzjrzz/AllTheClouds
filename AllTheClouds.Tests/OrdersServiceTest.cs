using System;
using System.Net.Http;
using System.Threading.Tasks;
using AllTheClouds.Models.DTO;
using AllTheClouds.Services;
using AutoFixture;
using Microsoft.Extensions.Logging;
using Moq;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;
using Xunit;

namespace AllTheClouds.Tests
{
    public class OrdersServiceTest
    {
        private readonly Fixture _fixture;
        private readonly HttpClient _httpClient;
        private readonly Mock<ILogger<OrdersService>> _mockLogger;

        public OrdersServiceTest()
        {
            _fixture = new Fixture();
            _httpClient = new HttpClient();
            _mockLogger = new Mock<ILogger<OrdersService>>();
        }

        [Fact]
        public async Task Given_Response_Is_Forbidden_When_Submitting_Order_Then_Log_The_Exception()
        {
            // Arrange
            using var server = WireMockServer.Start();
            _httpClient.BaseAddress = new Uri($"http://localhost:{server.Ports[0]}");
        
            server
                .Given(Request.Create().WithPath("/api/Orders").UsingPost())
                .RespondWith(Response.Create().WithStatusCode(403));
        
            var ordersService = new OrdersService(
                _httpClient,
                _mockLogger.Object);
        
            // Act
            await ordersService.SubmitOrderAsync(_fixture.Create<OrderItemsRequest>());
        
            // Assert
            _mockLogger.Verify(x => x.Log(
                It.IsAny<LogLevel>(),
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((o, t) => true),
                It.IsAny<Exception>(),
                It.Is<Func<It.IsAnyType, Exception, string>>((o, t) => true)), Times.Once);
        }
    }
}
