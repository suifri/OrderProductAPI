using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderProductAPI.DTO.Request;
using OrderProductAPI.DTO.Response;
using OrderProductAPI.Filters;
using OrderProductAPI.Repository.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace OrderProductAPI.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;
        private readonly IValidator<RequestOrderDTO> _validator;
        public OrderController(IProductRepository productRepository, IOrderRepository orderRepository, IValidator<RequestOrderDTO> validator)
        {
            _productRepository = productRepository;
            _orderRepository = orderRepository;
            _validator = validator;
        }

        [SwaggerOperation(
        Summary = "Create new order",
        Description = "Create new order from DTO with necessary fields"
        )]
        [HttpPost]
        [OrderPostExceptionFilterAttribute]
        public async Task<IActionResult> Post([SwaggerParameter("Input request order dto with necessary fields.")]RequestOrderDTO requestOrder)
        {

            ValidationResult validationResult = await _validator.ValidateAsync(requestOrder);

            if (validationResult.IsValid == false)
                return BadRequest(validationResult.Errors);

            return await _orderRepository.Create(requestOrder); 
        }

        [SwaggerOperation(
        Summary = "Returns all orders in database",
        Description = "Returns all orders in ResponseOrderDTO format"
        )]
        [HttpGet(Name = "GetAllOrders")]
        public async Task<IEnumerable<ResponseOrderDTO>> Get()
        {
            return await _orderRepository.Read();
        }

        [SwaggerOperation(
        Summary = "Returns single order by Id",
        Description = "Returns single order with searching Id in ResponseOrderDTO format"
        )]
        [HttpGet(Name = "GetById")]
        public async Task<ResponseOrderDTO> GetOrderById([SwaggerParameter("Searching orders Id")]int id)
        {
            var products = await _productRepository.ReadByOrderId(id);

            var order = await _orderRepository.Read(id);

            order.Products = products;

            return order;
        }

        [SwaggerOperation(
        Summary = "Returns orders by Product code",
        Description = "Returns orders with searching Product code in ResponseOrderDTO format"
        )]
        [HttpGet(Name = "GetByCode")]
        public async Task<IEnumerable<ResponseOrderDTO>> GetOrderByCode([SwaggerParameter("Searching product code")]string code)
        {
            return await _orderRepository.Read(code);
        }
    }
}
