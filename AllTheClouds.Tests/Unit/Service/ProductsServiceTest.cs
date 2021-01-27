using System;
using System.Net.Http;
using System.Threading.Tasks;
using AllTheClouds.Services;
using Microsoft.Extensions.Logging;
using Moq;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;
using Xunit;

namespace AllTheClouds.Tests.Unit.Service
{
    public class ProductsServiceTest
    {
        private readonly HttpClient _httpClient;
        private readonly Mock<ILogger<ProductsService>> _mockLogger;

        public ProductsServiceTest()
        {
            _httpClient = new HttpClient();
            _mockLogger = new Mock<ILogger<ProductsService>>();
        }

        [Fact]
        public async Task Given_Response_Is_Forbidden_When_Retrieving_Products_Then_Log_The_Exception()
        {
            // Arrange
            using var server = WireMockServer.Start();
            _httpClient.BaseAddress = new Uri($"http://localhost:{server.Ports[0]}");

            server
                .Given(Request.Create().WithPath("/api/Products").UsingGet())
                .RespondWith(Response.Create().WithStatusCode(403));

            var productsService = new ProductsService(
                _httpClient,
                _mockLogger.Object);

            // Act
            await productsService.ListProductsAsync();

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