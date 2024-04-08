using Microsoft.EntityFrameworkCore;
using OrderProductAPI.Models;

namespace OrderProductAPI.Contexts
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<Product> Products => Set<Product>();
        public DbSet<OrderProduct> OrderProducts => Set<OrderProduct>();
    }
}
