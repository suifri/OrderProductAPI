using OrderProductAPI.DTO.Response;

namespace OrderProductAPI.Repository.Interfaces
{
    public interface IOrderRepository
    {
        Task Create(ResponseOrderDTO order);

        Task<ResponseOrderDTO[]> Read();

        Task<ResponseOrderDTO> Read(int id);

        Task<ResponseOrderDTO> Read(string code);
    }
}
