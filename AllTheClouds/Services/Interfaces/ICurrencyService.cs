using System.Collections.Generic;
using System.Threading.Tasks;
using AllTheClouds.Models.DTO;

namespace AllTheClouds.Services.Interfaces
{
    public interface ICurrencyService
    {
        public Task<IEnumerable<ForeignExchangeRateResponse>> ListFxRatesAsync();
    }
}
