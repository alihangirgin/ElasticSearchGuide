using ElasticSearch.Api.Models;

namespace ElasticSearch.Api.Services
{
    public class ECommerceService : IECommerceService
    {
        private readonly IElasticSearchClient _elasticSearchClient;

        public ECommerceService(IElasticSearchClient elasticSearchClient)
        {
            _elasticSearchClient = elasticSearchClient;
        }

        public async Task<ApiResponse> TermQueryAsync(string fieldName, string value, bool caseInsensitive = true)
        {
            var response = await _elasticSearchClient.TermQueryAsync<ECommerce>("kibana_sample_data_ecommerce", fieldName, value, caseInsensitive);
            if (response == null) return ApiResponse.Failure();
            return ApiResponse.Success(response);
        }

        public async Task<ApiResponse> TermsQueryAsync(string fieldName, string[] values)
        {
            var response = await _elasticSearchClient.TermsQueryAsync<ECommerce>("kibana_sample_data_ecommerce", fieldName, values);
            if (response == null) return ApiResponse.Failure();
            return ApiResponse.Success(response);
        }

        public async Task<ApiResponse> PrefixQueryAsync(string fieldName, string prefix)
        {
            var response = await _elasticSearchClient.PrefixQueryAsync<ECommerce>("kibana_sample_data_ecommerce", fieldName, prefix);
            if (response == null) return ApiResponse.Failure();
            return ApiResponse.Success(response);
        }
        public async Task<ApiResponse> NumberRangeQueryAsync(string fieldName, double? from, double? to)
        {
            var response = await _elasticSearchClient.NumberRangeQueryAsync<ECommerce>("kibana_sample_data_ecommerce", fieldName, from, to);
            if (response == null) return ApiResponse.Failure();
            return ApiResponse.Success(response);
        }
        public async Task<ApiResponse> DateRangeQueryAsync(string fieldName, DateTime? from, DateTime? to)
        {
            var response = await _elasticSearchClient.DateRangeQueryAsync<ECommerce>("kibana_sample_data_ecommerce", fieldName, from, to);
            if (response == null) return ApiResponse.Failure();
            return ApiResponse.Success(response);
        }
        public async Task<ApiResponse> MatchAllQueryAsync()
        {
            var response = await _elasticSearchClient.MatchAllQueryAsync<ECommerce>("kibana_sample_data_ecommerce");
            if (response == null) return ApiResponse.Failure();
            return ApiResponse.Success(response);
        }
        public async Task<ApiResponse> MatchAllQueryWithPaginationAsync(int pageNumber, int pageSize, string? sortField, bool ascending = true)
        {
            var response = await _elasticSearchClient.MatchAllWithPaginationQueryAsync<ECommerce>("kibana_sample_data_ecommerce", pageSize, pageNumber, sortField, ascending);
            if (response == null) return ApiResponse.Failure();
            return ApiResponse.Success(response);
        }

        public async Task<ApiResponse> WildCardQueryAsync(string fieldName, string wildCardPattern)
        {
            var response = await _elasticSearchClient.WildcardQueryAsync<ECommerce>("kibana_sample_data_ecommerce", fieldName, wildCardPattern);
            if (response == null) return ApiResponse.Failure();
            return ApiResponse.Success(response);
        }

        public async Task<ApiResponse> FuzzyQueryAsync(string fieldName, string value, int? fuzziness)
        {
            var response = await _elasticSearchClient.FuzzyQueryAsync<ECommerce>("kibana_sample_data_ecommerce", fieldName, value, fuzziness);
            if (response == null) return ApiResponse.Failure();
            return ApiResponse.Success(response);
        }

        public async Task<ApiResponse> MatchQueryAsync(string fieldName, string value)
        {
            var response = await _elasticSearchClient.MatchQueryAsync<ECommerce>("kibana_sample_data_ecommerce", fieldName, value);
            if (response == null) return ApiResponse.Failure();
            return ApiResponse.Success(response);
        }

        public async Task<ApiResponse> MatchBoolPrefixQueryAsync(string fieldName, string value)
        {
            var response = await _elasticSearchClient.MatchBoolPrefixQueryAsync<ECommerce>("kibana_sample_data_ecommerce", fieldName, value);
            if (response == null) return ApiResponse.Failure();
            return ApiResponse.Success(response);
        }

        public async Task<ApiResponse> MatchPhraseQueryAsync(string fieldName, string phrase)
        {
            var response = await _elasticSearchClient.MatchPhraseQueryAsync<ECommerce>("kibana_sample_data_ecommerce", fieldName, phrase);
            if (response == null) return ApiResponse.Failure();
            return ApiResponse.Success(response);
        }

        public async Task<ApiResponse> MultiMatchQueryAsync(string[] fields, string query)
        {
            var response = await _elasticSearchClient.MultiMatchQueryAsync<ECommerce>("kibana_sample_data_ecommerce", fields, query);
            if (response == null) return ApiResponse.Failure();
            return ApiResponse.Success(response);
        }

        public async Task<ApiResponse> CompoundQueryAsync(string mustFieldName, string mustQuery, string shouldFieldName, string shouldQuery, string mustNotFieldName, string mustNotValue, string filterFieldName, string filterFrom, string filterTo)
        {
            var response = await _elasticSearchClient.CompoundQueryAsync<ECommerce>("kibana_sample_data_ecommerce", mustFieldName, mustQuery, shouldFieldName, shouldQuery, mustNotFieldName, mustNotValue, filterFieldName, filterFrom, filterTo);
            if (response == null) return ApiResponse.Failure();
            return ApiResponse.Success(response);
        }
    }
}
