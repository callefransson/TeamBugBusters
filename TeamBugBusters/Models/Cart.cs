using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeamBugBusters.Models
{
    public class Cart
    {
        [Key]
        public int CartId { get; set; }
        public string? UserId { get; set; }
    }
}
