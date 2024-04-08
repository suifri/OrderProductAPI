using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrderProductAPI.Models
{
    [Table("Order")]
    public class Order
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        [Required]
        public string CustomerFullName { get; set; } = null!;

        [Required]
        public string CustomerPhone { get; set; } = null!;
    }
}
