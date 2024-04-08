using OrderProductAPI.DTO;

namespace OrderProductAPI.Repository.Interfaces
{
    public interface IProductRepository
    {
        public Task Create(ProductDTO product);
        public Task Update(int id);
    }
}
