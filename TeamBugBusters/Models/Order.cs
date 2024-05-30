using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TeamBugBusters.Data;

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
        public double TotalPrice { get; set; }
        
        [Required]
        [StringLength(200, ErrorMessage = "Shipping adress can't be longer than 200 characters ")]
        public string ShippingAdress { get; set; }
        
        [Required]
        [StringLength(200, ErrorMessage = "Shipping adress can't be longer than 200 characters ")]
        public string City { get; set; }
        
        [Required]
        [Range(10000, 99999, ErrorMessage = "Please enter a valid zip code")]
        public string ZipCode { get; set; }

        [ForeignKey("Cart")]
        public int FkCartId { get; set; }
        public Cart? Cart { get; set; }
        public string? UserId { get; set; }
        public IdentityUser? User { get; set; }

        public string WeatherDescription { get; set; }
        public double Temperature { get; set; }
        public string WeatherIcon { get; set; }
    }
}
