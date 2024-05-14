using System.ComponentModel.DataAnnotations;

namespace TeamBugBusters.Models
{
    public class Admin
    {
        [Key]
        public int AdminId { get; set; }
        [Required]
        [StringLength(100, MinimumLength =10, ErrorMessage ="Full name can't be longer then 100 characters or less than 10")]
        public string AdminFullName { get; set; }
        [Required]
        [StringLength(100, MinimumLength =5, ErrorMessage = "Email can't be longer then 100 characters or less than 5")]
        public string AdminEmail { get; set; }
        [Required]
        [StringLength(20, MinimumLength =10, ErrorMessage ="Personal number has to be atleast 10 characters")]
        public string AdminPersonalNumber { get; set; }
        [Required]
        [StringLength(100,MinimumLength =5, ErrorMessage = "Password can't be longer then 100 characters or less than 5")]
        public string Password { get; set; }

        public ICollection<Product>? Products { get; set; }

    }
}
