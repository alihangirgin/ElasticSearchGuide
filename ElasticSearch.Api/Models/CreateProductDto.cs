namespace ElasticSearch.Api.Models
{
    public sealed record CreateProductDto(string Name, decimal Price, int Stock, ProductFeatureDto ProductFeature )
    {
        public Product CrateProduct()
        {
            return new Product()
            {
                Name = Name,
                Price = Price,
                Stock = Stock,
                Feature = new ProductFeature()
                {
                    Color = ProductFeature.Color,
                    Height = ProductFeature.Height,
                    Width = ProductFeature.Width
                }
            };
        }
    }
}
