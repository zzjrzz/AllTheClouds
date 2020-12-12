using System.Collections.Generic;
using System.Threading.Tasks;
using AllTheClouds.Models;

namespace AllTheClouds.Services
{
    interface IProductsService
    {
        Task<IEnumerable<ProductsResponse>> ListProductsAsync();
        Task<IEnumerable<ForeignExchangeRatesResponse>> ListFxRatesAsync();
        Task<string> SubmitOrderAsync(OrderItemsRequest orderItemsRequest);
    }
}
