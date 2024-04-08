using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderProductAPI.DTO.Request;
using OrderProductAPI.DTO.Response;
using OrderProductAPI.Repository.Interfaces;

namespace OrderProductAPI.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
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
        public async Task Post(RequestProductDTO requestProductDTO)
        {
            await _productRepository.Create(requestProductDTO);
        }

        [HttpPut]
        public async Task Put(int id, RequestProductDTO requestProductDTO)
        {
            await _productRepository.Update(id, requestProductDTO);
        }
    }
}
