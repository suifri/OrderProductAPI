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
            CreateMap<RequestOrderProductDTO, OrderProduct>(); 
            CreateMap<RequestOrderDTO, Order>()
                .ForMember(x => x.OrderProducts, o => o.MapFrom(y => y.orderProducts));


            CreateMap<OrderProduct, ResponseProductDTO>()
                .ForMember(x => x.Id, o => o.MapFrom(y => y.Product.Id))
                .ForMember(x => x.Name, o => o.MapFrom(y => y.Product.Name))
                .ForMember(x => x.Price, o => o.MapFrom(y => y.Product.Price))
                .ForMember(x => x.Code, o => o.MapFrom(y => y.Product.Code));

            CreateMap<Order, ResponseOrderDTO>()
                .ForMember(x => x.Products, o => o.MapFrom(y => y.OrderProducts));
        }
    }
}
