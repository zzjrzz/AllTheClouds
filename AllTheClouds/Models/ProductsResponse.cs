namespace AllTheClouds.Models
{
    public class ProductsResponse
    {
        public string ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal UnitPrice { get; set; }
        public int? MaximumQuantity { get; set; }
    }
}
