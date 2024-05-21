using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeamBugBusters.Models
{
    public class CartItems
    {
        [Key]
        public int CartItemsId { get; set; }
        [ForeignKey("Customer")]
        public int? FkCustomerId { get; set; }
        public Customer? Customer { get; set; }
        [ForeignKey("Product")]
        public int FkProductId { get; set; }
        public Product? Product { get; set; }
        [ForeignKey("Cart")]
        public int? FkCartId { get; set; }
        public Cart? Cart { get; set; }
        public int Quantity { get; set; }
        public int Discount { get; set; }
    }
}
