using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeamBugBusters.Models
{
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
        public int? ProductDiscount { get; set; }
        public int ProductPrice { get; set; }
        [ForeignKey("Category")]
        public int FkCategoryId { get; set; }
        public Category? Category { get; set; }
    }
}
