using System.Threading.Tasks;
using AllTheClouds.Models.DTO;
using AllTheClouds.Services;
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
        public async Task<ActionResult> Send_Order([FromBody] OrderItemsRequest orderItemsRequest)
        {
            var orderResponse = await _ordersService.SubmitOrderAsync(orderItemsRequest);

            if (!orderResponse.Equals("Order submitted"))
                return BadRequest();

            return Ok();
        }
    }
}
