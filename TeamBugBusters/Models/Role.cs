using System.ComponentModel.DataAnnotations;

namespace TeamBugBusters.Models
{
    public class Role
    {
        [Key]
        public int RoleId { get; set; }
        [Required]
        [StringLength(100, MinimumLength =5,ErrorMessage ="Role can't be longer than 100 characters or less then 5")]
        public string RoleName { get; set; }
        public ICollection<Admin>? Admin {  get; set; }
    }
}
