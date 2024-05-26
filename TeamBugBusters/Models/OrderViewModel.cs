namespace TeamBugBusters.Models
{
    public class OrderViewModel
    {
        public OrderStatus OrderStatus { get; set; }
        public int? TotalDiscount { get; set; }
        public Guid TrackingNumber { get; set; }
        public int OrderNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public int Items { get; set; }
        public double TotalPrice { get; set; }
        public string? UserId { get; set; }
        public int Quantity { get; set; }
        public int? Discount { get; set; }
        public int AmountOfItems { get; set; }
        public string ProductName { get; set; }
        public decimal? DiscountPrice { get; set; }
        public decimal ProductPrice { get; set; }
        public string ShippingAdress { get; set; }
        public string City { get; set; }
        public int ZipCode { get; set; }
    }
    
}
