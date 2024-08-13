using System.Text.Json.Serialization;

namespace ElasticSearch.Api.Models
{
    public class ECommerceProduct
    {
        [JsonPropertyName("product_id")]
        public long ProductId { get; set; }
        [JsonPropertyName("product_name")]
        public string ProductName { get; set; }
    }
}
