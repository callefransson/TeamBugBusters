using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeamBugBusters.Models
{
    public enum OrderStatus
    {
        Unconfirmed = 1,
        Confirmed,
        Shipped,
        Delivered

    }
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public int TotalDiscount { get; set; }
        public Guid TrackingNumber { get; set; }
        public int OrderNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public int Items { get; set; }
        public double TotalPrice { get; set; }
        [StringLength(200, ErrorMessage = "Shipping adress can't be longer than 200 characters ")]
        public string ShippingAdress { get; set; }
        [StringLength(200, ErrorMessage = "Shipping adress can't be longer than 200 characters ")]
        public string City { get; set; }
        public int ZipCode { get; set; }

        [ForeignKey("Cart")]
        public int FkCartId { get; set; }
        public Cart? Cart { get; set; }
    }
}
