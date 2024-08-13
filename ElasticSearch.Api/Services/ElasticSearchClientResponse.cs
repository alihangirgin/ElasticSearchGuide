using Elastic.Clients.Elasticsearch.Core.Search;
using ElasticSearch.Api.Models;

namespace ElasticSearch.Api.Services
{
    public static class ElasticSearchClientResponse
    {
        public static IEnumerable<T>? ToResponseItem<T>(this IReadOnlyCollection<Hit<T>> hits) where T : IEntity
        {
            return hits.Where(y => y is { Source: not null, Id: not null }).Select(x =>
                {
                    var responseItem = x.Source;
                    responseItem.Id = x.Id;
                    return responseItem;
                }
            ).ToList();
        }
    }
}
