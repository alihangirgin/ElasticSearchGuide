using System.Net;
using ElasticSearch.Api.Models;

namespace ElasticSearch.Api.Services
{
    public class ProductService : IProductService
    {
        private readonly IElasticSearchClient _elasticsearchClient;

        public ProductService(IElasticSearchClient elasticsearchClient)
        {
            _elasticsearchClient = elasticsearchClient;
        }

        public async Task<ApiResponse> CreateProductAsync(Product product)
        {
            var response = await _elasticsearchClient.IndexAsync(product, "products");
            if (response == null) return ApiResponse.Failure();
            return ApiResponse.Success(response);
        }

        public async Task<ApiResponse> UpdateProductAsync(string id, Product product)
        {
            var response = await _elasticsearchClient.UpdateDocumentAsync(id, product, "products");
            if (response == null) return ApiResponse.Failure();
            return ApiResponse.Success(response);
        }

        public async Task<ApiResponse> DeleteProductAsync(string id)
        {
            var response = await _elasticsearchClient.DeleteDocumentAsync<Product>(id, "products");
            if (!response) return ApiResponse.Failure();
            return ApiResponse.Success(response);
        }

        public async Task<ApiResponse> GetProductsAsync()
        {
            var response = await _elasticsearchClient.MatchAllQueryAsync<Product>("products");
            if (response == null) return ApiResponse.Failure();
            return ApiResponse.Success(response);
        }

        public async Task<ApiResponse> GetProductById(string id)
        {
            var response = await _elasticsearchClient.GetDocumentByIdAsync<Product>(id, "products");
            if (response == null) return ApiResponse.Failure();
            return ApiResponse.Success(response);
        }
    }
}
