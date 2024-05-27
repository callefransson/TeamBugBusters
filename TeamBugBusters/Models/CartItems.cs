using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeamBugBusters.Models
{
    public class CartItems
    {
        [Key]
        public int CartItemsId { get; set; }
        public string? UserId { get; set; }
        public IdentityUser? User { get; set; }
        [ForeignKey("Product")]
        public int FkProductId { get; set; }
        public Product? Product { get; set; }
        [ForeignKey("Cart")]
        public int? FkCartId { get; set; }
        public Cart? Cart { get; set; }
        public int Quantity { get; set; }
    }
}
