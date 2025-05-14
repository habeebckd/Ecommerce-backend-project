using E_Commerce.Dto.product;
using E_Commerce.Service.ProductService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductServices _services;
        public ProductController(IProductServices services)
        {
            _services = services;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("Add_Pro")]
        public async Task<IActionResult> AddProduct([FromForm] AddProductDto addProductDto, IFormFile image)
        
        {
            await _services.AddProduct(addProductDto, image);
            return Ok("Product Added Successfully");
        }

        [HttpGet("AllProducts")]
        public async Task<IActionResult> GetAllProducts()
        {
            var result = await _services.GetProducts();
            return Ok(result);
        }

        [HttpGet("featured")]
        public async Task<IActionResult> GetFeaturedProducts()
        {
            var result = await _services.FeaturedPro();
            return Ok(result);
        }

        [HttpGet("hot-deals")]
        public async Task<IActionResult> GetHotDeals()
        {
            var result = await _services.HotDeals();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var result = await _services.GetProductById(id);
            if (result == null)
                return NotFound("Product not found");
            return Ok(result);
        }

        [HttpGet("categoryName/{categoryName}")]
        public async Task<IActionResult> GetProductsByCategory(string categoryName)
        {
            var result = await _services.GetProductsByCategoryName(categoryName);
            if (result == null)
                return NotFound("No products found for this category");
            return Ok(result);
        }

        [Authorize(Roles ="Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var isDeleted = await _services.DeleteProduct(id);
            if (!isDeleted)
                return NotFound("Product not found");
            return Ok("Product deleted successfully");
        }

        [Authorize(Roles ="Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromForm] AddProductDto updatedProduct, IFormFile image)
        {
            await _services.UpdatePro(id, updatedProduct, image);
            return Ok("Product updated successfully");
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchProduct([FromQuery] string search)
        {
            var result = await _services.SearchProduct(search);
            return Ok(result);
        }
    }
}
