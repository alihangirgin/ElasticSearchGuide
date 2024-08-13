using ElasticSearch.Api.Models;
using ElasticSearch.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ElasticSearch.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ECommerceController : ControllerBase
    {
        private readonly IECommerceService _eCommerceService;

        public ECommerceController(IECommerceService eCommerceService)
        {
            _eCommerceService = eCommerceService;
        }
        [HttpGet("termLevel/termQuery")]
        public async Task<IActionResult> TermQueryAsync([FromQuery] string fieldName, [FromQuery] string value, [FromQuery] bool? caseInsensitive)
        {
            var response = await _eCommerceService.TermQueryAsync(fieldName, value, caseInsensitive ?? true);
            return Ok(response);
        }

        [HttpPost("termLevel/termsQuery")]
        public async Task<IActionResult> TermsQueryAsync([FromBody] TermsQueryRequestModel model)
        {
            var response = await _eCommerceService.TermsQueryAsync(model.FieldName, model.Values);
            return Ok(response);
        }

        [HttpGet("termLevel/prefixQuery")]
        public async Task<IActionResult> PrefixQueryAsync([FromQuery] string fieldName, [FromQuery] string prefix)
        {
            var response = await _eCommerceService.PrefixQueryAsync(fieldName, prefix);
            return Ok(response);
        }

        [HttpGet("termLevel/numberRangeQuery")]
        public async Task<IActionResult> NumberRangeQueryAsync([FromQuery] string fieldName, [FromQuery] double? from, [FromQuery] double? to)
        {
            var response = await _eCommerceService.NumberRangeQueryAsync(fieldName, from, to);
            return Ok(response);
        }
        [HttpGet("termLevel/dateRangeQuery")]
        public async Task<IActionResult> DateRangeQueryAsync([FromQuery] string fieldName, [FromQuery] DateTime? from, [FromQuery] DateTime? to)
        {
            var response = await _eCommerceService.DateRangeQueryAsync(fieldName, from, to);
            return Ok(response);
        }
        [HttpGet("termLevel/matchAllQuery")]
        public async Task<IActionResult> MatchAllQueryAsync()
        {
            var response = await _eCommerceService.MatchAllQueryAsync();
            return Ok(response);
        }
        [HttpGet("termLevel/matchAllQueryWithPagination")]
        public async Task<IActionResult> MatchAllQueryWithPagination([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 20, [FromQuery] string sortField = "customer_full_name.keyword", [FromQuery] bool ascending = true)
        {
            var response = await _eCommerceService.MatchAllQueryWithPaginationAsync(pageNumber, pageSize,sortField, ascending);
            return Ok(response);
        }

        [HttpGet("termLevel/wildCardQuery")]
        public async Task<IActionResult> WildCardQueryAsync([FromQuery] string fieldName, [FromQuery] string wildCardPattern)
        {
            var response = await _eCommerceService.WildCardQueryAsync(fieldName, wildCardPattern);
            return Ok(response);
        }

        [HttpGet("termLevel/fuzzyQueryAsync")]
        public async Task<IActionResult> FuzzyQueryAsync([FromQuery] string fieldName, [FromQuery] string value, [FromQuery] int? fuzziness)
        {
            var response = await _eCommerceService.FuzzyQueryAsync(fieldName, value, fuzziness);
            return Ok(response);
        }

        [HttpGet("fullText/matchQuery")]
        public async Task<IActionResult> MatchQueryAsync([FromQuery] string fieldName, [FromQuery] string value)
        {
            var response = await _eCommerceService.MatchQueryAsync(fieldName, value);
            return Ok(response);
        }

        [HttpGet("fullText/matchBoolPrefixQuery")]
        public async Task<IActionResult> MatchBoolPrefixQueryAsync([FromQuery] string fieldName, [FromQuery] string value)
        {
            var response = await _eCommerceService.MatchBoolPrefixQueryAsync(fieldName, value);
            return Ok(response);
        }

        [HttpGet("fullText/matchPhraseQuery")]
        public async Task<IActionResult> MatchPhraseQueryAsync([FromQuery] string fieldName, [FromQuery] string phrase)
        {
            var response = await _eCommerceService.MatchPhraseQueryAsync(fieldName, phrase);
            return Ok(response);
        }

        [HttpPost("fullText/matchPhraseQuery")]
        public async Task<IActionResult> MultiMatchQueryAsync([FromBody] MultiMatchQueryRequestModel model)
        {
            var response = await _eCommerceService.MultiMatchQueryAsync(model.Fields, model.Query);
            return Ok(response);
        }

        [HttpGet("fullText/compoundQuery")]
        public async Task<IActionResult> CompoundQueryAsync([FromQuery] string mustFieldName, [FromQuery] string mustQuery, [FromQuery] string shouldFieldName,
            [FromQuery] string shouldQuery, [FromQuery] string mustNotFieldName, [FromQuery] string mustNotValue, [FromQuery] string filterFieldName, [FromQuery] string filterFrom,
            [FromQuery] string filterTo)
        {
            var response = await _eCommerceService.CompoundQueryAsync(mustFieldName, mustQuery, shouldFieldName, shouldQuery, mustNotFieldName, mustNotValue, filterFieldName, filterFrom, filterTo);
            return Ok(response);
        }
    }
}
