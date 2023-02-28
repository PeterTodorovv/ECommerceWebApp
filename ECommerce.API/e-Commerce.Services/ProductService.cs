using AutoMapper;
using e_Commerce.Data.Entities;
using e_Commerce.Data.Repository;
using e_Commerce.Services.Interfaces;
using e_Commerce.Services.Models.Pagination;
using e_Commerce.Services.Requests;
using e_Commerce.Services.Responses;
using FootballApp.Services.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
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
        private IConfiguration _config;

        public ProductService(IRepository<Product> productsRepository, IMapper mapper, IUserService userService, IConfiguration configuration)
        {
            _repository = productsRepository;
            _mapper = mapper;
            _userService = userService;
            _config = configuration;
        }

        public async Task<Product> CreateProduct(ProductRequest productRequest)
        {
            var image = productRequest.Image;
            if (image == null || image.Length == 0)
            {
                throw new BusinessError("image is not uploaded successfully", System.Net.HttpStatusCode.BadRequest);
            }

            var product = _mapper.Map<Product>(productRequest);

            if (_repository.Exist(x => x.Name == product.Name))
            {
                throw new BusinessError("Product with this name already exists", System.Net.HttpStatusCode.BadRequest);
            }

            var path = Path.Combine(_config.GetSection("ImagePath:ImageFolderPath").Value, image.FileName);

            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                await image.CopyToAsync(stream);
                stream.Close();
            }


            product.ImageAddress = image.FileName;

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

        public async Task<ProductAllResponse> GetAll(BasePagination paginationParams)
        {
            var products = await _repository.AllAsNoTracking().ToListAsync();

            if (!paginationParams.searchParam.IsNullOrEmpty())
                products = products.Where(c => c.Name.ToLower().Contains(paginationParams.searchParam.ToLower())).ToList();

            switch (paginationParams.orderBy.ToLower())
            {
                case ("name"):
                    products = paginationParams.isAscending ? products.OrderBy(c => c.Name).ThenBy(c => c.Price).ToList()
                        : products.OrderByDescending(c => c.Name).ThenBy(c => c.Price).ToList();
                    break;
                case ("price"):
                    products = paginationParams.isAscending ? products.OrderBy(c => c.Price).ThenBy(c => c.Name).ToList()
                        : products.OrderByDescending(c => c.Price).ThenBy(c => c.Name).ToList();
                    break;
                default:
                    products = products.OrderBy(c => c.Price).ToList();
                    break;
            }


            int count = products.Count;
            products = products.Skip((paginationParams.Page - 1) * paginationParams.ItemsPerPage).Take(paginationParams.ItemsPerPage).ToList();

            var mappedProducts = _mapper.Map<List<Product>>(products);
            var productsAllResponse = new ProductAllResponse() { Products= mappedProducts, ProductsCount = count };

            return productsAllResponse;
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
