using OrderProductAPI.DTO.Request;
using OrderProductAPI.Models;

namespace OrderProductAPI.Repository.Interfaces
{
    public interface IOrderProductRepository
    {
        Task Create(IEnumerable<RequestOrderProductDTO> orderProducts, int orderId);
    }
}
