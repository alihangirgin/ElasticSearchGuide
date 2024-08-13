using System.Text.Json.Serialization;

namespace ElasticSearch.Api.Models
{
    public class ECommerce : IEntity
    {
        [JsonPropertyName("customer_first_name")]
        public string CustomerFirstName { get; set; }
        [JsonPropertyName("customer_last_name")]
        public string CustomerLastName { get; set; }
        [JsonPropertyName("customer_full_name")]
        public string CustomerFullName { get; set; }
        [JsonPropertyName("category")]
        public string[] Category { get; set; }
        [JsonPropertyName("order_id")]
        public int OrderId { get; set; }
        [JsonPropertyName("order_date")]
        public DateTime OrderDate { get; set; }
        [JsonPropertyName("products")]
        public ECommerceProduct[] Products { get; set; }
    }
}
