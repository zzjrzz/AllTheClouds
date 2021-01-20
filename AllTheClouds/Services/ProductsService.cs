using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using AllTheClouds.Models.DTO;
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

        private const string ListProductsUrl = "/api/Products";
        private const string ListForeignExchangeRatesUrl = "/api/fx-rates";

        public ProductsService(HttpClient client, IConfiguration configuration, ILogger<ProductsService> logger)
        {
            Configuration = configuration;
            Client = client;
            _allTheCloudsApiKey = Configuration["AllTheClouds:ApiKey"];
            _logger = logger;
            ConfigureHttpClient();
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

        public async Task<IEnumerable<ForeignExchangeRateResponse>> ListFxRatesAsync()
        {
            var response = await Client.GetAsync(ListForeignExchangeRatesUrl);

            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException httpException)
            {
                _logger.LogWarning(
                    $"Failed call to {ListForeignExchangeRatesUrl} with status code {response.StatusCode}",
                    httpException);
            }

            var apiResponse = await response.Content.ReadAsStringAsync();
            var fxRates = JsonConvert.DeserializeObject<List<ForeignExchangeRateResponse>>(apiResponse);
            return fxRates;
        }

        private void ConfigureHttpClient()
        {
            Client.BaseAddress = new Uri(Configuration["AllTheClouds:BaseAddress"]);

            if (!Client.DefaultRequestHeaders.Contains("api-key"))
                Client.DefaultRequestHeaders.Add("api-key", _allTheCloudsApiKey);
        }
    }
}