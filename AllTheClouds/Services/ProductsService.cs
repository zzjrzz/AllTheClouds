using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
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
        private readonly string _baseAddress;

        private const string ListProductsUrl = "/api/Products";
        private const string ListForeignExchangeRatesUrl = "/api/fx-rates";
        private const string SubmitOrderUrl = "/api/Orders";

        public ProductsService(HttpClient client, IConfiguration configuration, ILogger<ProductsService> logger)
        {
            Client = client;
            Configuration = configuration;
            _logger = logger;
            _allTheCloudsApiKey = Configuration["AllTheClouds:ApiKey"];
            _baseAddress = Configuration["AllTheClouds:BaseAddress"];
        }

        public async Task<IEnumerable<ProductResponse>> ListProductsAsync()
        {
            if (_baseAddress == null)
                throw new NullReferenceException();

            Client.BaseAddress = new Uri(_baseAddress);
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
            var products = JsonConvert.DeserializeObject<List<ProductResponse>>(apiResponse);
            return products;
        }

        public async Task<IEnumerable<ForeignExchangeRateResponse>> ListFxRatesAsync()
        {
            if (_baseAddress == null)
                throw new NullReferenceException();

            Client.BaseAddress = new Uri(_baseAddress);
            Client.DefaultRequestHeaders.Add("api-key", _allTheCloudsApiKey);
            var response = await Client.GetAsync(ListForeignExchangeRatesUrl);

            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException httpException)
            {
                _logger.LogWarning(
                    $"Failed call to {ListForeignExchangeRatesUrl} with status code {response.StatusCode}", httpException);
            }

            var apiResponse = await response.Content.ReadAsStringAsync();
            var fxRates = JsonConvert.DeserializeObject<List<ForeignExchangeRateResponse>>(apiResponse);
            return fxRates;
        }

        public async Task<string> SubmitOrderAsync(OrderItemsRequest orderItemsRequest)
        {
            if (_baseAddress == null)
                throw new NullReferenceException();

            Client.BaseAddress = new Uri(_baseAddress);
            Client.DefaultRequestHeaders.Add("api-key", _allTheCloudsApiKey);

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