using System.Text.Json.Serialization;

namespace ElasticSearch.Api.Models
{
    public class Product : IEntity
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public ProductFeature Feature { get; set; }
    }
}
