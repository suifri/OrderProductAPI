﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OrderProductAPI.Contexts;
using OrderProductAPI.DTO.Request;
using OrderProductAPI.DTO.Response;
using OrderProductAPI.Models;
using OrderProductAPI.Repository.Interfaces;
using System.Runtime.CompilerServices;

namespace OrderProductAPI.Repository.Implementations
{
    public class ProductRepository : IProductRepository
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;
        public ProductRepository(IMapper mapper, ApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task Create(RequestProductDTO product)
        {
            var sql = @"INSERT INTO Product (Code, Name, Price) VALUES ({0}, {1}, {2})";

            await _context.Database.ExecuteSqlRawAsync(sql, product.Code, product.Name, product.Price);
        }

        public async Task<ResponseProductDTO[]> Read()
        {
            var sql = @"SELECT * FROM Product";

            var products = await _context.Products.FromSqlRaw(sql).ToListAsync();

            return _mapper.Map<ResponseProductDTO[]>(products);
        }

        public async Task<ResponseProductDTO> Read(int id)
        {
            var sql = @"SELECT * FROM Product WHERE Id = {0}";

            var product = await _context.Products.FromSqlRaw(sql, id).FirstOrDefaultAsync();

            return _mapper.Map<Product, ResponseProductDTO>(product);
        }

        public async Task<ResponseProductDTO> Read(decimal price)
        {
            var sql = @"SELECT * FROM Product WHERE Price = {0}";

            var product = await _context.Products.FromSqlRaw(sql, price).FirstOrDefaultAsync();

            return _mapper.Map<Product, ResponseProductDTO>(product);
        }

        public async Task Update(int id, RequestProductDTO updatedProduct)
        {
            var sql = @"UPDATE Product SET Code = {0}, Name = {1}, Price = {2} WHERE Id = {3}";

            await _context.Database.ExecuteSqlRawAsync(sql, updatedProduct.Code, updatedProduct.Name, updatedProduct.Price, id);
        }

    }
}