using ElasticSearch.Api.Models;

namespace ElasticSearch.Api.Services
{
    public interface IElasticSearchClient
    {
        Task<object?> IndexAsync<T>(T entityObject, string indexName) where T : IEntity;
        Task<object?> MatchAllQueryAsync<T>(string indexName) where T : IEntity;
        Task<object?> MatchAllWithPaginationQueryAsync<T>(string indexName, int pageSize, int pageNumber, string sortField, bool ascending) where T : IEntity;
        Task<object?> GetDocumentByIdAsync<T>(string id, string indexName) where T : IEntity;
        Task<object?> UpdateDocumentAsync<T>(string id, T entityObject, string indexName) where T : IEntity;
        Task<bool> DeleteDocumentAsync<T>(string id, string indexName) where T : IEntity;
        Task<object?> TermQueryAsync<T>(string indexName, string fieldName, string value, bool caseInsensitive) where T : IEntity;
        Task<object?> TermsQueryAsync<T>(string indexName, string fieldName, string[] values) where T : IEntity;
        Task<object?> PrefixQueryAsync<T>(string indexName, string fieldName, string prefix) where T : IEntity;
        Task<object?> NumberRangeQueryAsync<T>(string indexName, string fieldName, double? from, double? to) where T : IEntity;
        Task<object?> DateRangeQueryAsync<T>(string indexName, string fieldName, DateTime? from, DateTime? to) where T : IEntity;
        Task<object?> WildcardQueryAsync<T>(string indexName, string fieldName, string wildcardPattern) where T : IEntity;
        Task<IEnumerable<T>?> FuzzyQueryAsync<T>(string indexName, string fieldName, string value, int? fuzziness) where T : IEntity;
        Task<object?> MatchQueryAsync<T>(string indexName, string fieldName, string value) where T : IEntity;
        Task<IEnumerable<T>?> MatchBoolPrefixQueryAsync<T>(string indexName, string fieldName, string query) where T : IEntity;
        Task<IEnumerable<T>?> MatchPhraseQueryAsync<T>(string indexName, string fieldName, string phrase) where T : IEntity;
        Task<IEnumerable<T>?> MultiMatchQueryAsync<T>(string indexName, string[] fields, string query) where T : IEntity;
        Task<IEnumerable<T>?> CompoundQueryAsync<T>(string indexName, string mustFieldName, string mustQuery,
            string shouldFieldName, string shouldQuery, string mustNotFieldName, string mustNotValue,
            string filterFieldName, string filterFrom, string filterTo) where T : IEntity;
    }
}
