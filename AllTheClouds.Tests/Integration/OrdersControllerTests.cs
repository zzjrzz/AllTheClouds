using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AllTheClouds.Models.DTO;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Xunit;

namespace AllTheClouds.Tests.Integration
{
    public class OrdersControllerTests :
        IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public OrdersControllerTests(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Theory]
        [InlineData("/api/Orders")]
        public async Task InvalidRequestReturnsBadRequest(string url)
        {
            var client = _factory.CreateClient();
            var orderItemsRequest = new OrderItemsRequest();
            var request = new StringContent(JsonConvert.SerializeObject(orderItemsRequest), Encoding.UTF8,
                "application/json");
            var response = await client.PostAsync(url, request);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}