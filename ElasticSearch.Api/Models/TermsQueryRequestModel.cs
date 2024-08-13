namespace ElasticSearch.Api.Models
{
    public class TermsQueryRequestModel
    {
        public string FieldName { get; set; }
        public string[] Values { get; set; }
    }
}
