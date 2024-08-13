namespace ElasticSearch.Api.Models
{
    public class MultiMatchQueryRequestModel
    {
        public string[] Fields { get; set; }
        public string Query { get; set; }
    }
}
