using System.Collections.Generic;

namespace AllTheClouds.Models
{
    public class OrderItem
    {
        public string ProductId;
        public int Quantity;
    }

    public class OrderItemsRequest
    {
        public string CustomerName;
        public string CustomerEmail;
        public List<OrderItem> LineItems;
    }
}
