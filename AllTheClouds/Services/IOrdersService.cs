using System.Threading.Tasks;
using AllTheClouds.Models.DTO;

namespace AllTheClouds.Services
{
    public interface IOrdersService
    {
        Task<string> SubmitOrderAsync(OrderItemsRequest orderItemsRequest);
    }
}
