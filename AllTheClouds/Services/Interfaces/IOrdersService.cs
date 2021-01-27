using System.Threading.Tasks;
using AllTheClouds.Models.DTO;

namespace AllTheClouds.Services.Interfaces
{
    public interface IOrdersService
    {
        Task<string> SubmitOrderAsync(OrderItemsRequest orderItemsRequest);
    }
}
