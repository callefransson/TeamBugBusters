namespace TeamBugBusters.Models
{
    public class OrderViewModel
    {
        public int OrderNumber { get; set; }
        public Guid TrackingNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public string ShippingAddress { get; set; }
        public OrderStatus OrderStatus { get; set; }
    }
}
