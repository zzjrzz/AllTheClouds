﻿using System;
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
        public void Given_Empty_Base_Address_Then_Throw_Null_Exception()
        {
            Assert.Throws<ArgumentNullException>(() => new ProductsService(
                _httpClient,
                _mockConfiguration.Object,
                _mockLogger.Object));
        }

        [Fact]
        public async Task Given_Response_Is_Forbidden_When_Retrieving_Products_Then_Log_The_Exception()
        {
            // Arrange
            using var server = WireMockServer.Start();
            var testBaseAddress = new Uri($"http://localhost:{server.Ports[0]}");
            _mockConfiguration
                .Setup(configuration => configuration["AllTheClouds:BaseAddress"])
                .Returns(testBaseAddress.ToString());

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
            var testBaseAddress = new Uri($"http://localhost:{server.Ports[0]}");
            _mockConfiguration
                .Setup(configuration => configuration["AllTheClouds:BaseAddress"])
                .Returns(testBaseAddress.ToString());


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
    }
}