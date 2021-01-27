using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using AllTheClouds.Models.DTO;
using AllTheClouds.Services.Interfaces;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace AllTheClouds.Services
{
    public class ProductsService : IProductsService
    {
        private HttpClient Client { get; }

        private readonly ILogger<ProductsService> _logger;

        private const string ListProductsUrl = "/api/Products";
        private const string ListForeignExchangeRatesUrl = "/api/fx-rates";

        public ProductsService(HttpClient client, ILogger<ProductsService> logger)
        {
            Client = client;
            _logger = logger;
        }

        public async Task<IEnumerable<ProductResponse>> ListProductsAsync()
        {
            var response = await Client.GetAsync(ListProductsUrl);

            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException httpException)
            {
                _logger.LogWarning(
                    $"Failed call to {ListProductsUrl} with status code {response.StatusCode}", httpException);
            }

            var apiResponse = await response.Content.ReadAsStringAsync();
            var products = JsonConvert.DeserializeObject<List<ProductResponse>>(apiResponse);
            return products;
        }
    }
}