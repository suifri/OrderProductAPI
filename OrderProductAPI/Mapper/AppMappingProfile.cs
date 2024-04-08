using AutoMapper;
using OrderProductAPI.DTO.Request;
using OrderProductAPI.DTO.Response;
using OrderProductAPI.Models;

namespace OrderProductAPI.Mapper
{
    public class AppMappingProfile : Profile
    {
        public AppMappingProfile() 
        {
            CreateMap<RequestProductDTO, Product>();
            CreateMap<Product, ResponseProductDTO>();
        }
    }
}
