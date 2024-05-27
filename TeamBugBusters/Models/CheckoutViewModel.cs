using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TeamBugBusters.Models
{
    public class CheckoutViewModel
    {
        public OrderStatus OrderStatus { get; set; }
        public int? TotalDiscount { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        [DisplayFormat(DataFormatString = "{0:0.##}")]
        public decimal TotalPrice { get; set; }
        public int Quantity { get; set; }
        public int? Discount { get; set; }
        public int AmountOfItems { get; set; }
        public string ProductName { get; set; }
        public decimal? DiscountPrice { get; set; }
        public decimal ProductPrice { get; set; }
    }
    
}
