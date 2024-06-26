﻿using OrderProductAPI.DTO.Request;
using OrderProductAPI.DTO.Response;

namespace OrderProductAPI.Repository.Interfaces
{
    public interface IProductRepository
    {
        public Task Create(RequestProductDTO product);
        public Task Update(int id, RequestProductDTO updatedProduct);
        public Task<IEnumerable<ResponseProductDTO>> Read();
        public Task<ResponseProductDTO> Read(int id);
        public Task<IEnumerable<ResponseProductDTO>> Read(decimal price);
        public Task<IEnumerable<ResponseProductDTO>> ReadByOrderId(int orderId);
    }
}
