namespace SalesAndService.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string ?OrderId { get; set; }
        public int ProductId { get; set; }
        public Product ?Product { get; set; }
        public int CustomerId { get; set; }
        public Customer ?Customer { get; set; }
        public string ?Region { get; set; }
        public DateTime DateOfSale { get; set; }
        public int QuantitySold { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }
        public decimal ShippingCost { get; set; }
        public string ?PaymentMethod { get; set; }
    }
}
