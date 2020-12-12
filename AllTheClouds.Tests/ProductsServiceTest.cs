using System;
using System.Net.Http;
using System.Threading.Tasks;
using AllTheClouds.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;
using Xunit;

namespace AllTheClouds.Tests
{
    public class ProductsServiceTest
    {
        private const int Port = 80;
        private readonly Uri _testBaseAddress = new Uri($"http://localhost:{Port}");
        private readonly HttpClient _httpClient;
        private readonly Mock<IConfiguration> _mockConfiguration;
        private readonly Mock<ILogger<ProductsService>> _mockLogger;

        public ProductsServiceTest()
        {
            _httpClient = new HttpClient {BaseAddress = _testBaseAddress};
            _mockConfiguration = new Mock<IConfiguration>();
            _mockLogger = new Mock<ILogger<ProductsService>>();
        }

        [Fact]
        public async Task Given_Empty_Base_Address_When_Retrieving_Products_Then_Throw_Null_Exception()
        {
            // Arrange
            var productsService = new ProductsService(
                _httpClient,
                _mockConfiguration.Object,
                _mockLogger.Object);

            // Act
            await Assert.ThrowsAsync<NullReferenceException>(() => productsService.ListProductsAsync());
        }

        [Fact]
        public async Task Given_Response_Is_Forbidden_When_Retrieving_Products_Then_Log_The_Exception()
        {
            // Arrange
            _mockConfiguration
                .Setup(configuration => configuration["AllTheClouds:BaseAddress"])
                .Returns(_testBaseAddress.ToString());

            using var server = WireMockServer.Start(Port);
            server
                .Given(Request.Create().WithPath("/api/products").UsingGet())
                .RespondWith(Response.Create().WithStatusCode(403));

            var productsService = new ProductsService(
                _httpClient,
                _mockConfiguration.Object,
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