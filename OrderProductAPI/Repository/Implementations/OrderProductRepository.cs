using Microsoft.EntityFrameworkCore;
using OrderProductAPI.Contexts;
using OrderProductAPI.DTO.Request;
using OrderProductAPI.Repository.Interfaces;

namespace OrderProductAPI.Repository.Implementations
{
    public class OrderProductRepository : IOrderProductRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task Create(IEnumerable<RequestOrderProductDTO> orderProducts, int orderId)
        {
            foreach (var order in orderProducts)
            {
                var sql = @"INSERT INTO OrderProduct (Amount, TotalPrice, ProductId, OrderId) VALUES ({0}, (SELECT Price FROM Product WHERE Id = {1}) * {0}, {1}, {2})";
                await _context.Database.ExecuteSqlRawAsync(sql, order.Amount, order.ProductId, orderId);
            }
        }
    }
}
