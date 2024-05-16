using System.ComponentModel.DataAnnotations;

namespace TeamBugBusters.Models
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }
        [Required]
        [StringLength(100, ErrorMessage ="Full name can't be loonger than 100 characters")]
        public string CustomerFullName { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "Adress can't be longer than 100 characters")]
        public string CustomerAdress { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Password can't be longer then 100 characters or less than 5")]
        [DataType(DataType.Password)]
        public string CustomerPassword { get; set; }
        [Required]
        [StringLength(20, ErrorMessage ="Phone number can't be longer than 20 characters")]
        public string CustomerPhone { get; set; }
        [Required]
        [StringLength(20, ErrorMessage = "Personal number can't be longer than 20 characters")]
        public string CustomerPersonalNumber { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Email can't be longer then 100 characters or less than 5")]
        public string CustomerEmail { get; set; }
    }
}
