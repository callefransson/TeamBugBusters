using System.ComponentModel.DataAnnotations;

namespace TeamBugBusters.Models
{
    public class EditProductViewModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int CategoryId { get; set; }
        public string ProductDescription { get; set; }
        public int ProductStock { get; set; }
        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Please enter a valid price")]
        public decimal ProductPrice { get; set; }
    }
}
