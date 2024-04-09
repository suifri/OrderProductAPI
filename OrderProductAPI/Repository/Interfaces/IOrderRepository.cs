using Microsoft.AspNetCore.Mvc;
using OrderProductAPI.DTO.Request;
using OrderProductAPI.DTO.Response;

namespace OrderProductAPI.Repository.Interfaces
{
    public interface IOrderRepository
    {
        Task<IActionResult> Create(RequestOrderDTO order);

        Task<IEnumerable<ResponseOrderDTO>> Read();

        Task<ResponseOrderDTO> Read(int id);

        Task<IEnumerable<ResponseOrderDTO>> Read(string code);
    }
}
