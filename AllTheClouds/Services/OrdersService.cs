using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AllTheClouds.Models.DTO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace AllTheClouds.Services
{
    public class OrdersService : IOrdersService
    {
        private IConfiguration Configuration { get; }
        private HttpClient Client { get; }

        private readonly ILogger<OrdersService> _logger;
        private readonly string _allTheCloudsApiKey;

        private const string SubmitOrderUrl = "/api/Orders";

        public OrdersService(HttpClient client, IConfiguration configuration, ILogger<OrdersService> logger)
        {
            Configuration = configuration;
            Client = client;
            _allTheCloudsApiKey = Configuration["AllTheClouds:ApiKey"];
            _logger = logger;
            ConfigureHttpClient();
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

        private void ConfigureHttpClient()
        {
            Client.BaseAddress = new Uri(Configuration["AllTheClouds:BaseAddress"]);

            if (!Client.DefaultRequestHeaders.Contains("api-key"))
                Client.DefaultRequestHeaders.Add("api-key", _allTheCloudsApiKey);
        }
    }
}
