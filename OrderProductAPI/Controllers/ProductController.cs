using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderProductAPI.DTO.Request;
using OrderProductAPI.DTO.Response;
using OrderProductAPI.Filters;
using OrderProductAPI.Repository.Interfaces;
using OrderProductAPI.Validators;

namespace OrderProductAPI.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly IValidator<RequestProductDTO> _validator;
        public ProductController(IProductRepository productRepository, IValidator<RequestProductDTO> validator)
        {
            _productRepository = productRepository;
            _validator = validator;
        }

        [HttpGet(Name = "GetAllProducts")]
        public async Task<ResponseProductDTO[]> GetProducts()
        {
            var products = await _productRepository.Read();

            return products;
        }

        [HttpGet(Name = "GetProductById")]
        public async Task<ResponseProductDTO> GetProductById(int id)
        {
            var product = await _productRepository.Read(id);

            return product;
        }

        [HttpGet(Name = "GetProductByCost")]
        public async Task<ResponseProductDTO> GetProductByCost(decimal cost)
        {
            var product = await _productRepository.Read(cost);

            return product;
        }

        [HttpPost]
        [ProductPostExceptionFilterAttribute]
        public async Task<IActionResult> Post(RequestProductDTO requestProductDTO)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(requestProductDTO);

            if (validationResult.IsValid == false)
                return BadRequest(validationResult.Errors);

            await _productRepository.Create(requestProductDTO);

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Put(int id, RequestProductDTO requestProductDTO)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(requestProductDTO);

            if (validationResult.IsValid == false)
                return BadRequest(validationResult.Errors);

            await _productRepository.Update(id, requestProductDTO);

            return Ok();    
        }
    }
}
