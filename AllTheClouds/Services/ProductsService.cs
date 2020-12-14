using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AllTheClouds.Models;
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
        private const string SubmitOrderUrl = "/api/Orders";

        public ProductsService(HttpClient client, IConfiguration configuration, ILogger<ProductsService> logger)
        {
            Configuration = configuration;
            Client = client;
            _logger = logger;

            Client.BaseAddress = new Uri(Configuration["AllTheClouds:BaseAddress"]);
            _allTheCloudsApiKey = Configuration["AllTheClouds:ApiKey"];
        }

        public async Task<IEnumerable<ProductResponse>> ListProductsAsync()
        {
            var response = await GetAsyncWithApiKey(ListProductsUrl);

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
            var response = await GetAsyncWithApiKey(ListForeignExchangeRatesUrl);

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

        public async Task<string> SubmitOrderAsync(OrderItemsRequest orderItemsRequest)
        {
            var request = new StringContent(JsonConvert.SerializeObject(orderItemsRequest), Encoding.UTF8,
                "application/json");
            var response = await PostAsyncWithApiKey(SubmitOrderUrl, request);

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

        private async Task<HttpResponseMessage> PostAsyncWithApiKey(string path, HttpContent request)
        {
            if (!Client.DefaultRequestHeaders.Contains("api-key"))
                Client.DefaultRequestHeaders.Add("api-key", _allTheCloudsApiKey);
            return await Client.PostAsync(path, request);
        }

        private async Task<HttpResponseMessage> GetAsyncWithApiKey(string path)
        {
            if (!Client.DefaultRequestHeaders.Contains("api-key"))
                Client.DefaultRequestHeaders.Add("api-key", _allTheCloudsApiKey);
            return await Client.GetAsync(path);
        }
    }
}