using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrderProductAPI.Models
{
    [Table("OrderProduct")]
    public class OrderProduct
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Amount { get; set; }

        [Required]
        [Precision(10, 2)]
        [Range(0.1, double.MaxValue, ErrorMessage = "Price must be more than 0")]
        public decimal TotalPrice { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public Product? Product { get; set; }

        [ForeignKey("Order")]
        public int OrderId { get; set; }
        public Order? Order { get; set; }
    }
}
