using System.Collections.Generic;
using System.Threading.Tasks;
using AllTheClouds.Models.DTO;

namespace AllTheClouds.Services.Interfaces
{
    public interface IProductsService
    {
        Task<IEnumerable<ProductResponse>> ListProductsAsync();
    }
}
