using System.Threading.Tasks;
using AllTheClouds.Models.DTO;
using AllTheClouds.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AllTheClouds.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrdersService _ordersService;

        public OrdersController(IOrdersService ordersService)
        {
            _ordersService = ordersService;
        }

        [HttpPost]
        public async Task<ActionResult> SendOrder([FromBody] OrderItemsRequest orderItemsRequest)
        {
            var orderResponse = await _ordersService.SubmitOrderAsync(orderItemsRequest);

            if (!orderResponse.Equals("Order submitted"))
                return BadRequest();

            return Ok();
        }
    }
}
