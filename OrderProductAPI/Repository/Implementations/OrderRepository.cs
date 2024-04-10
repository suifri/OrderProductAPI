using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderProductAPI.Contexts;
using OrderProductAPI.DTO.Request;
using OrderProductAPI.DTO.Response;
using OrderProductAPI.Models;
using OrderProductAPI.Repository.Interfaces;

namespace OrderProductAPI.Repository.Implementations
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;
        private readonly IOrderProductRepository _orderProductRepository;
        public OrderRepository(IMapper mapper, ApplicationDbContext applicationDbContext, IOrderProductRepository orderProductRepository)
        {
            _mapper = mapper;
            _context = applicationDbContext;
            _orderProductRepository = orderProductRepository;
        }

        public async Task<IActionResult> Create(RequestOrderDTO order)
        {
            foreach (var item in order.orderProducts)
                if ((await _context.Products.AnyAsync(x => x.Id == item.ProductId) == false))
                    return new BadRequestResult();

            var newOrder = _mapper.Map<Order>(order);
            await _context.Orders.AddAsync(newOrder);
            await _context.SaveChangesAsync();

            await _orderProductRepository.Create(order.orderProducts, newOrder.Id);

            return new OkResult();
        }

        public async Task<IEnumerable<ResponseOrderDTO>> Read()
        {
            var res = await _context.Orders.Include(x => x.OrderProducts).ThenInclude(x => x.Product).ToListAsync();

            return _mapper.Map<IEnumerable<ResponseOrderDTO>>(res);
        }

        public async Task<ResponseOrderDTO> Read(int id)
        {
            var sqlSelectOrder = @"SELECT * FROM [Order] WHERE Id={0}";

            var order = _mapper.Map<ResponseOrderDTO>((await _context.Orders.FromSqlRaw<Order>(sqlSelectOrder, id).ToListAsync()).FirstOrDefault());

            var sqlSelectProducts = @"SELECT p.Id, p.Code, p.Name, p.Price FROM OrderProduct op INNER JOIN Product p ON p.Id = op.ProductId WHERE OrderId={0}";

            var products = _mapper.Map<IEnumerable<ResponseProductDTO>>(await _context.Products.FromSqlRaw(sqlSelectProducts, id).ToListAsync());

            order.Products = products;

            return order;
        }

        public async Task<IEnumerable<ResponseOrderDTO>> Read(string code)
        {
            var res = await _context.Orders.Include(x => x.OrderProducts).ThenInclude(x => x.Product).Where(o => o.OrderProducts.Any(op => op.Product.Code == code)).ToListAsync();
 
            return _mapper.Map<IEnumerable<ResponseOrderDTO>>(res);
        }
    }
}
