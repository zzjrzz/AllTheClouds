using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace AllTheClouds.Tests.Integration
{
    public class ProductsControllerTests :
        IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public ProductsControllerTests(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Theory]
        [InlineData("/api/Products?markupMultiplier=1.2")]
        [InlineData("/api/Products?markupMultiplier=1.2&targetCurrency=AUD")]
        [InlineData("/api/Products?markupMultiplier=1.2&sourceCurrency=USD&targetCurrency=AUD")]
        public async Task CheckEndpoints(string url)
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync(url);

            response.EnsureSuccessStatusCode();
            Assert.Equal("application/json; charset=utf-8",
                response.Content.Headers.ContentType?.ToString());
        }
    }
}