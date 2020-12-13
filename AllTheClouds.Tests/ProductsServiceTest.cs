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
        private Uri _testBaseAddress;
        private readonly HttpClient _httpClient;
        private readonly Mock<IConfiguration> _mockConfiguration;
        private readonly Mock<ILogger<ProductsService>> _mockLogger;

        public ProductsServiceTest()
        {
            _httpClient = new HttpClient();
            _mockConfiguration = new Mock<IConfiguration>();
            _mockLogger = new Mock<ILogger<ProductsService>>();
        }

        [Fact]
        public async Task Given_Empty_Base_Address_Then_Throw_Null_Exception()
        {
            // Arrange
            var productsService = new ProductsService(
                _httpClient,
                _mockConfiguration.Object,
                _mockLogger.Object);

            // Act
            await Assert.ThrowsAsync<NullReferenceException>(() => productsService.ListProductsAsync());
            await Assert.ThrowsAsync<NullReferenceException>(() => productsService.ListFxRatesAsync());
            await Assert.ThrowsAsync<NullReferenceException>(() =>
                productsService.SubmitOrderAsync(new Models.OrderItemsRequest()));
        }

        [Fact]
        public async Task Given_Response_Is_Forbidden_When_Retrieving_Products_Then_Log_The_Exception()
        {
            // Arrange
            using var server = WireMockServer.Start();
            _testBaseAddress = new Uri($"http://localhost:{server.Ports[0]}");
            _mockConfiguration
                .Setup(configuration => configuration["AllTheClouds:BaseAddress"])
                .Returns(_testBaseAddress.ToString());

            server
                .Given(Request.Create().WithPath("/api/Products").UsingGet())
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

        [Fact]
        public async Task Given_Response_Is_Forbidden_When_Retrieving_FxRates_Then_Log_The_Exception()
        {
            // Arrange
            using var server = WireMockServer.Start();
            _testBaseAddress = new Uri($"http://localhost:{server.Ports[0]}");
            _mockConfiguration
                .Setup(configuration => configuration["AllTheClouds:BaseAddress"])
                .Returns(_testBaseAddress.ToString());


            server
                .Given(Request.Create().WithPath("/api/fx-rates").UsingGet())
                .RespondWith(Response.Create().WithStatusCode(403));

            var productsService = new ProductsService(
                _httpClient,
                _mockConfiguration.Object,
                _mockLogger.Object);

            // Act
            await productsService.ListFxRatesAsync();

            // Assert
            _mockLogger.Verify(x => x.Log(
                It.IsAny<LogLevel>(),
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((o, t) => true),
                It.IsAny<Exception>(),
                It.Is<Func<It.IsAnyType, Exception, string>>((o, t) => true)), Times.Once);
        }

        [Fact]
        public async Task Given_Response_Is_Forbidden_When_Submitting_Order_Then_Log_The_Exception()
        {
            // Arrange
            using var server = WireMockServer.Start();
            _testBaseAddress = new Uri($"http://localhost:{server.Ports[0]}");
            _mockConfiguration
                .Setup(configuration => configuration["AllTheClouds:BaseAddress"])
                .Returns(_testBaseAddress.ToString());

            server
                .Given(Request.Create().WithPath("/api/Orders").UsingPost())
                .RespondWith(Response.Create().WithStatusCode(403));

            var productsService = new ProductsService(
                _httpClient,
                _mockConfiguration.Object,
                _mockLogger.Object);

            // Act
            await productsService.SubmitOrderAsync(new Models.OrderItemsRequest());

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