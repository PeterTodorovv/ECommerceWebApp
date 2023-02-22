using AutoMapper;
using e_Commerce.Data.Entities;
using e_Commerce.Data.Repository;
using e_Commerce.Services.Interfaces;
using e_Commerce.Services.Requests;
using FootballApp.Services.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_Commerce.Services
{
    public class ProductService : IProductService
    {
        private IRepository<Product> _repository;
        private IUserService _userService;
        private readonly IMapper _mapper;

        public ProductService(IRepository<Product> productsRepository, IMapper mapper, IUserService userService)
        {
            _repository = productsRepository;
            _mapper = mapper;
            _userService = userService;
        }

        public async Task<Product> CreateProduct(ProductRequest productRequest)
        {
            var product = _mapper.Map<Product>(productRequest);

            if(_repository.Exist(x => x.Name == product.Name))
            {
                throw new BusinessError("Product with this name already exists.", System.Net.HttpStatusCode.BadRequest);
            }

            await _repository.AddAsync(product);
            await _repository.SaveChangesAsync();

            return product;
        }

        public async Task<Product> Get(Guid id)
        {
            var product = _repository.GetFirstBy(x => x.Id == id);

            if (product == null)
            {
                throw new BusinessError("This product does not exist!", System.Net.HttpStatusCode.NotFound);
            }

            return product;
        }

        public async Task<List<Product>> GetAll()
        {
            var products = _repository.AllAsNoTracking().ToList();

            return products;
        }

        public async Task<Product> AddProductToCart(Guid productId, Guid userId)
        {
            var product = _repository.GetFirstBy(x => x.Id == productId);
            var user = await _userService.Get(userId);

            user.ShoppingCart.Add(product);

            return product;
        }
    }
}
