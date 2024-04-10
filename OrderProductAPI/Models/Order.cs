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
        public DateTime CreatedOn { get; set; } = DateTime.Now;

        [Required]
        [RegularExpression(@"^(?=.*\s)[^\d\s]*$", ErrorMessage = "Invalid customer name")]
        public string CustomerFullName { get; set; } = null!;

        [Required]
        [Phone]
        public string CustomerPhone { get; set; } = null!;

        public IEnumerable<OrderProduct> OrderProducts { get; set; } = null!;
    }
}
