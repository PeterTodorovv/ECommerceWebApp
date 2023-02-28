using e_Commerce.Data.Entities;
using e_Commerce.Services.Interfaces;
using e_Commerce.Services.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using e_Commerce.Services.Models.Pagination;

namespace e_Commerce_Website.Controllers
{
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            this._productService = productService;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Post([FromForm] ProductRequest productRequest)
        {
            var product = await _productService.CreateProduct(productRequest);
            return CreatedAtAction(nameof(Get), _productService);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var product = await _productService.Get(id);

            return Ok(product);
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] BasePagination paginationParams)
        {
            var product = await _productService.GetAll(paginationParams);

            return Ok(product);
        }

    }
}
