using System.Collections.Generic;

namespace AllTheClouds.Models.DTO
{
    public class OrderItem
    {
        public string ProductId { get; set; }
        public int Quantity { get; set; }
    }

    public class OrderItemsRequest
    {
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public List<OrderItem> LineItems { get; set; }
    }
}
