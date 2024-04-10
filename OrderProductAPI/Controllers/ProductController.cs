using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderProductAPI.DTO.Request;
using OrderProductAPI.DTO.Response;
using OrderProductAPI.Filters;
using OrderProductAPI.Repository.Interfaces;
using OrderProductAPI.Validators;
using Swashbuckle.AspNetCore.Annotations;

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

        [SwaggerOperation(
            Summary = "Get a list of all evailable products.",
            Description = "Retrives an array of products"
            )]

        [HttpGet(Name = "GetAllProducts")]
        public async Task<IEnumerable<ResponseProductDTO>> GetProducts()
        {
            var products = await _productRepository.Read();

            return products;
        }

        [SwaggerOperation(
        Summary = "Get a specific product by Id.",
        Description = "Retrives a single product with specific Id"
        )]
        [HttpGet(Name = "GetProductById")]
        public async Task<ResponseProductDTO> GetProductById([SwaggerParameter("Id of searching product")]int id)
        {
            var product = await _productRepository.Read(id);

            return product;
        }

        [SwaggerOperation(
        Summary = "Get products by cost.",
        Description = "Retrives products with specific cost"
        )]
        [HttpGet(Name = "GetProductByCost")]
        public async Task<IEnumerable<ResponseProductDTO>> GetProductByCost([SwaggerParameter("Cost of searching product")]decimal cost)
        {
            var products = await _productRepository.Read(cost);

            return products;
        }

        [SwaggerOperation(
        Summary = "Create new product",
        Description = "Create new product from DTO"
        )]
        [HttpPost]
        [ProductPostExceptionFilterAttribute]
        public async Task<IActionResult> Post([SwaggerParameter("DTO that represents all necesarry data of product")]RequestProductDTO requestProductDTO)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(requestProductDTO);

            if (validationResult.IsValid == false)
                return BadRequest(validationResult.Errors);

            await _productRepository.Create(requestProductDTO);

            return Ok();
        }

        [SwaggerOperation(
        Summary = "Update existing product",
        Description = "Update fully or only some data of product in database"
        )]
        [HttpPut]
        public async Task<IActionResult> Put([SwaggerParameter("Id of updating parameter")]int id,
            [SwaggerParameter("DTO that represent necessary data of product")]RequestProductDTO requestProductDTO)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(requestProductDTO);

            if (validationResult.IsValid == false)
                return BadRequest(validationResult.Errors);

            await _productRepository.Update(id, requestProductDTO);

            return Ok();    
        }
    }
}
