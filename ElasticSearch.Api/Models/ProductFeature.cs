namespace ElasticSearch.Api.Models
{
    public class ProductFeature
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public ColorEnum Color { get; set; }
    }

    public enum ColorEnum 
    {
        Red = 0,
        Green = 1,
        Blue = 2
    }
}
