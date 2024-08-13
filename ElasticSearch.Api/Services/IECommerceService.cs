using ElasticSearch.Api.Models;
using System.Threading.Tasks;

namespace ElasticSearch.Api.Services
{
    public interface IECommerceService
    {
        Task<ApiResponse> TermQueryAsync(string fieldName, string value, bool caseInsensitive = true);
        Task<ApiResponse> TermsQueryAsync(string fieldName, string[] values);
        Task<ApiResponse> PrefixQueryAsync(string fieldName, string prefix);
        Task<ApiResponse> NumberRangeQueryAsync(string fieldName, double? from, double? to);
        Task<ApiResponse> DateRangeQueryAsync(string fieldName, DateTime? from, DateTime? to);
        Task<ApiResponse> MatchAllQueryAsync();
        Task<ApiResponse> MatchAllQueryWithPaginationAsync(int pageNumber, int pageSize, string? sortField, bool ascending = true);
        Task<ApiResponse> WildCardQueryAsync(string fieldName, string wildCardPattern);
        Task<ApiResponse> FuzzyQueryAsync(string fieldName, string value, int? fuzziness);
        Task<ApiResponse> MatchQueryAsync(string fieldName, string value);
        Task<ApiResponse> MatchBoolPrefixQueryAsync(string fieldName, string value);
        Task<ApiResponse> MatchPhraseQueryAsync(string fieldName, string phrase);
        Task<ApiResponse> MultiMatchQueryAsync(string[] fields, string query);
        Task<ApiResponse> CompoundQueryAsync(string mustFieldName, string mustQuery, string shouldFieldName,
            string shouldQuery, string mustNotFieldName, string mustNotValue, string filterFieldName, string filterFrom,
            string filterTo);
    }
}
