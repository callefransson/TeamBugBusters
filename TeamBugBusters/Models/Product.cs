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
        public int? ProductStock {  get; set; }
        public int? ProductDiscount { get; set; }
        [DataType(DataType.Date)]
        public DateTime? DiscountStartDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime? DiscountEndDate { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        [DisplayFormat(DataFormatString = "{0:0.##}")]
        public decimal? DiscountPrice { get; set; }
        public string? ProductImage { get; set; }
        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        [DisplayFormat(DataFormatString = "{0:0.##}")]
        public decimal ProductPrice { get; set; }
        [ForeignKey("Category")]
        public int FkCategoryId { get; set; }
        public Category? Category { get; set; }

        public bool IsDiscountActive() // Created this bool to check if discount is active or not
        {
            return DiscountPrice.HasValue && DiscountStartDate.HasValue && DiscountEndDate.HasValue &&
                   DateTime.Now >= DiscountStartDate.Value && DateTime.Now <= DiscountEndDate.Value;
        }
    }
}
