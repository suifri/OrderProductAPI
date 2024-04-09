using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderProductAPI.DTO.Request;
using OrderProductAPI.DTO.Response;
using OrderProductAPI.Filters;
using OrderProductAPI.Repository.Interfaces;

namespace OrderProductAPI.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IValidator<RequestOrderDTO> _validator;
        public OrderController(IOrderRepository orderRepository, IValidator<RequestOrderDTO> validator)
        {
            _orderRepository = orderRepository;
            _validator = validator;
        }

        [HttpPost]
        [OrderPostExceptionFilterAttribute]
        public async Task<IActionResult> Post(RequestOrderDTO requestOrder)
        {
            throw new NotImplementedException();

            ValidationResult validationResult = await _validator.ValidateAsync(requestOrder);

            if (validationResult.IsValid == false)
                return BadRequest(validationResult.Errors);

            return await _orderRepository.Create(requestOrder); 
        }

        [HttpGet(Name = "GetAllOrders")]
        public async Task<IEnumerable<ResponseOrderDTO>> Get()
        {
            return await _orderRepository.Read();
        }

        [HttpGet(Name = "GetById")]
        public async Task<ResponseOrderDTO> GetOrderById(int id)
        {
            return await _orderRepository.Read(id);
        }

        [HttpGet(Name = "GetByCode")]
        public async Task<IEnumerable<ResponseOrderDTO>> GetOrderByCode(string code)
        {
            return await _orderRepository.Read(code);
        }
    }
}
