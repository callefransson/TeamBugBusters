using System.ComponentModel.DataAnnotations;

namespace TeamBugBusters.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        [Required]
        [StringLength(100)]
        public string CategoryName { get; set; }
        public ICollection<Product>? Products { get; set; }
    }
}
