using System.Collections.Generic;
using System.Threading.Tasks;
using AllTheClouds.Models.DTO;

namespace AllTheClouds.Services
{
    public interface ICurrencyService
    {
        public Task<IEnumerable<ForeignExchangeRateResponse>> ListFxRatesAsync();
    }
}
