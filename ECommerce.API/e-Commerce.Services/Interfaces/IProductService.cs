using e_Commerce.Data.Entities;
using e_Commerce.Services.Models.Pagination;
using e_Commerce.Services.Requests;
using e_Commerce.Services.Responses;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_Commerce.Services.Interfaces
{
    public interface IProductService
    {
        public Task<Product> CreateProduct(ProductRequest productRequest);
        public Task<ProductAllResponse> GetAll(BasePagination paginationParams);
        public Task<Product> Get(Guid id);
        public Task<Product> AddProductToCart(Guid productId, Guid userId);

    }
}
