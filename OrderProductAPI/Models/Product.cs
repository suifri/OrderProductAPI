using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrderProductAPI.Models
{
    [Table("Product")]
    public class Product
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Code { get; set; } = null!;

        [Required]
        [MaxLength(200)]
        [RegularExpression(@"^[^\d]*$", ErrorMessage = "Invalid product name")]
        public string Name { get; set; } = null!;

        [Required]
        [Precision(10,2)]
        [Range(0.1, double.MaxValue, ErrorMessage = "Number must be more than 0")]
        public decimal Price { get; set; }

        public OrderProduct? OrderProduct { get; set; } 
    }
}
