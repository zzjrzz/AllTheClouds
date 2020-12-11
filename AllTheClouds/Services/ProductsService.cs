using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using AllTheClouds.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace AllTheClouds.Services
{
    public class ProductsService : IProductsService
    {
        private IConfiguration Configuration { get; }
        private HttpClient Client { get; }
        private readonly ILogger<ProductsService> _logger;
        private readonly string _allTheCloudsApiKey;
        private const string BaseAddress = "http://alltheclouds.com.au";
        private const string ListProductsUrl = "/api/products";

        public ProductsService(HttpClient client, IConfiguration configuration, ILogger<ProductsService> logger)
        {
            Client = client;
            Configuration = configuration;
            _logger = logger;
            _allTheCloudsApiKey = Configuration["AllTheClouds:ApiKey"];
        }

        public async Task<IEnumerable<ProductsResponse>> ListProductsAsync()
        {
            Client.BaseAddress = new System.Uri(BaseAddress);
            Client.DefaultRequestHeaders.Add("api-key", _allTheCloudsApiKey);
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
            var products = JsonConvert.DeserializeObject<List<ProductsResponse>>(apiResponse);
            return products;
        }
    }
}