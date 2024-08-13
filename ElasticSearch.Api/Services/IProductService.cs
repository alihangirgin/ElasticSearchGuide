using ElasticSearch.Api.Models;

namespace ElasticSearch.Api.Services
{
    public interface IProductService
    {
        Task<ApiResponse> CreateProductAsync(Product product);
        Task<ApiResponse> UpdateProductAsync(string id, Product product);
        Task<ApiResponse> DeleteProductAsync(string id);
        Task<ApiResponse> GetProductsAsync();
        Task<ApiResponse> GetProductById(string id);
    }
}
