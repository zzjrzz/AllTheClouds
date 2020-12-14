using System.Collections.Generic;
using System.Threading.Tasks;
using AllTheClouds.Models;
using AllTheClouds.Models.DTO;

namespace AllTheClouds.Services
{
    interface IProductsService
    {
        Task<IEnumerable<ProductResponse>> ListProductsAsync();
        Task<IEnumerable<ForeignExchangeRateResponse>> ListFxRatesAsync();
        Task<string> SubmitOrderAsync(OrderItemsRequest orderItemsRequest);
    }
}
