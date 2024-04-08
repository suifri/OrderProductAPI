using OrderProductAPI.DTO;

namespace OrderProductAPI.Repository.Interfaces
{
    public interface IOrderRepository
    {
        Task Create(OrderDTO order);

        Task<OrderDTO[]> Read();

        Task<OrderDTO> Read(int id);

        Task<OrderDTO> Read(string code);
    }
}
