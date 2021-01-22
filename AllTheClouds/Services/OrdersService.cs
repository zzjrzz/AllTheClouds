using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AllTheClouds.Models.DTO;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace AllTheClouds.Services
{
    public class OrdersService : IOrdersService
    {
        private HttpClient Client { get; }

        private readonly ILogger<OrdersService> _logger;

        private const string SubmitOrderUrl = "/api/Orders";

        public OrdersService(HttpClient client, ILogger<OrdersService> logger)
        {
            Client = client;
            _logger = logger;
        }

        public async Task<string> SubmitOrderAsync(OrderItemsRequest orderItemsRequest)
        {
            var request = new StringContent(JsonConvert.SerializeObject(orderItemsRequest), Encoding.UTF8,
                "application/json");
            var response = await Client.PostAsync(SubmitOrderUrl, request);

            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException httpException)
            {
                _logger.LogWarning(
                    $"Failed call to {SubmitOrderUrl} with status code {response.StatusCode}", httpException);
            }

            return await response.Content.ReadAsStringAsync();
        }
    }
}
