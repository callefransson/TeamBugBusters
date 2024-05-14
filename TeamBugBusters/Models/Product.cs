using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeamBugBusters.Models
{
    public enum Category
    {
        Pc = 1,
        Keyboard,
        Mouse,
        Monitor,
        MonitorArm,
        MousePad
    }
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "Product name can't be longer than 100 characters")]
        public string ProductName { get; set; }
        [Required]
        [StringLength(250, ErrorMessage = "Description can't be longer than 250 characters")]
        public string ProductDescription { get; set; }
        public int ProductStock {  get; set; }
        public Category Category { get; set; }
        public int ProductDiscount { get; set; }
        public int ProductPrice { get; set; }
        public ICollection<Cart>? Carts { get; set; }
        [ForeignKey("Admin")]
        public int FkAdminId { get; set; }
        public Admin? Admin { get; set; }

    }
}
