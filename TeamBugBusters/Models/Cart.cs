using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeamBugBusters.Models
{
    public class Cart
    {
        [Key]
        public int CartId { get; set; }
        public int TotalPrice { get; set; }
        public int AmountOfItems { get; set; }
        public int? TotalDiscount { get; set; }
        //[ForeignKey("Customer")]
        //public int FkCustomerId { get; set; }
        //public Customer? Customer { get; set; }
        //[ForeignKey("Product")]
        //public int FkProductId { get; set; }
        //public Product? Product { get; set; }'
        public ICollection<CartItems>? CartItems { get; set; }
    }
}
