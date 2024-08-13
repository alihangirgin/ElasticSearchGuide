using System.Text.Json.Serialization;

namespace ElasticSearch.Api.Models
{
    public class IEntity
    {
        [JsonPropertyName("_id")]
        public string Id { get; set; }
        [JsonPropertyName("created_on")]
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
