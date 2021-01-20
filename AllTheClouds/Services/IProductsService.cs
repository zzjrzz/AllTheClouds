using System.Collections.Generic;
using System.Threading.Tasks;
using AllTheClouds.Models.DTO;

namespace AllTheClouds.Services
{
    public interface IProductsService
    {
        Task<IEnumerable<ProductResponse>> ListProductsAsync();
        Task<IEnumerable<ForeignExchangeRateResponse>> ListFxRatesAsync();
    }
}
