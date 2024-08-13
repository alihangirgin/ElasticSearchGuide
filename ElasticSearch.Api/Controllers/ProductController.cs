using ElasticSearch.Api.Models;
using ElasticSearch.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ElasticSearch.Api.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpPost]
        public async Task<IActionResult> CreateProductAsync(CreateProductDto model)
        {
            var response = await _productService.CreateProductAsync(model.CrateProduct());
            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProductAsync(string id, CreateProductDto model)
        {
            var response = await _productService.UpdateProductAsync(id, model.CrateProduct());
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductAsybc(string id)
        {
            var response = await _productService.DeleteProductAsync(id);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductAsync(string id)
        {
            var response = await _productService.GetProductById(id);
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetProductsAsync()
        {
            var response = await _productService.GetProductsAsync();
            return Ok(response);
        }
    }
}
