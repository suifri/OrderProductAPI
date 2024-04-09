using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderProductAPI.Contexts;
using OrderProductAPI.DTO.Request;
using OrderProductAPI.DTO.Response;
using OrderProductAPI.Models;
using OrderProductAPI.Repository.Interfaces;
using System.Runtime.CompilerServices;

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

            var newOrder = new Order { CustomerFullName = order.CustomerFullName, CustomerPhone = order.CustomerPhone};
            await _context.Orders.AddAsync(newOrder);
            await _context.SaveChangesAsync();

            await _orderProductRepository.Create(order.orderProducts, newOrder.Id);

            return new OkResult();
        }

        private List<ResponseOrderDTO> responseOrderDTOsConverter(List<Order> orders)
        {
            var resultList = new List<ResponseOrderDTO>();

            foreach (var item in orders)
            {
                Dictionary<int, string> map = new Dictionary<int, string>();

                for (int i = 0; i < item.OrderProducts.Count(); i++)
                    map.Add(item.OrderProducts.ElementAt(i).Product.Id, item.OrderProducts.ElementAt(i).Product.Name);

                resultList.Add(new ResponseOrderDTO { Id = item.Id, CustomerFullName = item.CustomerFullName, CreatedOn = item.CreatedOn, CustomerPhone = item.CustomerPhone, ProductInformation = map });
            }

            return resultList;
        }

        private ResponseOrderDTO responseOrderDTOConverter(Order order)
        {
            Dictionary<int, string> map = new Dictionary<int, string>();

            for (int i = 0; i < order.OrderProducts.Count(); i++)
                map.Add(order.OrderProducts.ElementAt(i).Product.Id, order.OrderProducts.ElementAt(i).Product.Name);

            return new ResponseOrderDTO { Id = order.Id, CreatedOn = order.CreatedOn, CustomerFullName = order.CustomerFullName, CustomerPhone = order.CustomerPhone, ProductInformation = map };
        }

        public async Task<ResponseOrderDTO[]> Read()
        {
            var res = await _context.Orders.Include(x => x.OrderProducts).ThenInclude(x => x.Product).ToListAsync();

            return responseOrderDTOsConverter(res).ToArray();
        }

        public async Task<ResponseOrderDTO> Read(int id)
        {
            var res = await _context.Orders.Include(x => x.OrderProducts).ThenInclude(x => x.Product).Where(x => x.Id == id).FirstAsync();

            return responseOrderDTOConverter(res);
        }

        public async Task<IEnumerable<ResponseOrderDTO>> Read(string code)
        {
            var res = await _context.Orders.Include(x => x.OrderProducts).ThenInclude(x => x.Product).Where(o => o.OrderProducts.Any(op => op.Product.Code == code)).ToListAsync();

            return responseOrderDTOsConverter(res);
        }
    }
}
