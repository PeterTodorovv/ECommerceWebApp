using e_Commerce.Data.Entities;
using e_Commerce.Services.Interfaces;
using e_Commerce.Services.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace e_Commerce_Website.Controllers
{
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            this._productService = productService;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ProductRequest productRequest)
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
        public async Task<IActionResult> Get()
        {
            var product = await _productService.GetAll();

            return Ok(product);
        }

        [HttpPost("{AddToCart}")]
        public async Task<IActionResult> AddToCart([FromBody] Guid productId)
        {
            var userId = Guid.Parse(HttpContext.User.FindFirstValue("Id"));
            var product = await _productService.AddProductToCart(productId, userId);

            return Ok(product);
        }
    }
}
