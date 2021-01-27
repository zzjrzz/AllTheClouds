using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using AllTheClouds.Models.DTO;
using AllTheClouds.Services.Interfaces;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace AllTheClouds.Services
{
    public class CurrencyService : ICurrencyService
    {
        private HttpClient Client { get; }

        private readonly ILogger<CurrencyService> _logger;

        private const string ListForeignExchangeRatesUrl = "/api/fx-rates";

        public CurrencyService(HttpClient client, ILogger<CurrencyService> logger)
        {
            Client = client;
            _logger = logger;
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
    }
}
