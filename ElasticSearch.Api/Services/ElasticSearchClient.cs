using Elastic.Clients.Elasticsearch;
using Elastic.Transport;
using ElasticSearch.Api.Models;
using Elastic.Clients.Elasticsearch.QueryDsl;

namespace ElasticSearch.Api.Services
{
    public class ElasticSearchClient : IElasticSearchClient
    {
        private readonly ElasticsearchClient _client;
        public ElasticSearchClient(ElasticSearchConfig config)
        {
            var settings = new ElasticsearchClientSettings(new Uri(config.Url));
            settings.Authentication(new BasicAuthentication(config.Username, config.Password));
            _client = new ElasticsearchClient(settings);

        }

        public async Task<object?> IndexAsync<T>(T entityObject, string indexName) where T : IEntity
        {
            entityObject.CreatedAt = DateTime.Now;
            var response = await _client.IndexAsync(entityObject, x => x.Index(indexName));

            if (response is not { IsValidResponse: true }) return null;
            entityObject.Id = response.Id;
            return entityObject;
        }

        public async Task<object?> MatchAllQueryAsync<T>(string indexName) where T : IEntity
        {
            var matchAllQuery = new MatchAllQuery();
            var response = await _client.SearchAsync<T>(s => s
                .Index(indexName)
                .Query(q => q
                        .MatchAll(matchAllQuery)
                )
            );

            if (response is not { IsValidResponse: true }) return null;
            return response.Hits.Where(y => y is { Source: not null, Id: not null }).Select(x =>
                {
                    var responseItem = x.Source;
                    responseItem.Id = x.Id;
                    return responseItem;
                }
            ).ToList();
        }

        public async Task<object?> MatchAllWithPaginationQueryAsync<T>(string indexName, int pageSize, int pageNumber, string sortField, bool ascending) where T : IEntity
        {
            var matchAllQuery = new MatchAllQuery();
            var response = await _client.SearchAsync<T>(s => s
                .Index(indexName)
                .Query(q => q
                    .MatchAll(matchAllQuery)
                )
                .Sort(sort => sort
                    .Field(sortField, new FieldSort() { Order = ascending ? SortOrder.Asc : SortOrder.Desc })

                )
                .From(pageSize * (pageNumber - 1))//skip
                .Size(pageSize)//take
            );

            if (response is not { IsValidResponse: true }) return null;
            return response.Hits.Where(y => y is { Source: not null, Id: not null }).Select(x =>
                {
                    var responseItem = x.Source;
                    responseItem.Id = x.Id;
                    return responseItem;
                }
            ).ToList();
        }

        public async Task<object?> GetDocumentByIdAsync<T>(string id, string indexName) where T : IEntity
        {
            var response = await _client.GetAsync<T>(id, g => g.Index(indexName));
            if (response is not { IsValidResponse: true } || response.Source == null) return null;
            var documentResponse = response.Source;
            documentResponse.Id = response.Id;
            return documentResponse;
        }

        public async Task<object?> UpdateDocumentAsync<T>(string id, T entityObject, string indexName) where T : IEntity
        {
            entityObject.UpdatedAt = DateTime.Now;
            var response = await _client.UpdateAsync<T, T>(id, u => u.Index(indexName).Doc(entityObject));

            if (response is not { IsValidResponse: true }) return null;
            entityObject.Id = response.Id;
            return entityObject;
        }

        public async Task<bool> DeleteDocumentAsync<T>(string id, string indexName) where T : IEntity
        {
            var response = await _client.DeleteAsync<T>(id, d => d.Index(indexName));

            if (response is not { IsValidResponse: true }) return false;
            return true;
        }

        public async Task<object?> TermQueryAsync<T>(string indexName, string fieldName, string value, bool caseInsensitive) where T : IEntity
        {
            var response = await _client.SearchAsync<T>(s => s
                .Index(indexName)
                .Query(q => q
                    .Term(t => t
                        .Field(fieldName)
                        .Value(value)
                        .CaseInsensitive(caseInsensitive)
                    )
                )
            );

            if (response is not { IsValidResponse: true }) return null;
            return response.Hits.Where(y => y is { Source: not null, Id: not null }).Select(x =>
                {
                    var responseItem = x.Source;
                    responseItem.Id = x.Id;
                    return responseItem;
                }
            ).ToList();
        }

        public async Task<object?> TermsQueryAsync<T>(string indexName, string fieldName, string[] values) where T : IEntity
        {
            List<FieldValue> terms = new();
            values.ToList().ForEach(x => terms.Add(x));

            var response = await _client.SearchAsync<T>(s => s
                .Index(indexName)
                .Query(q => q
                    .Terms(t => t
                        .Field(fieldName)
                        .Term(new TermsQueryField(terms))
                    )
                )
            );

            if (response is not { IsValidResponse: true }) return null;
            return response.Hits.Where(y => y is { Source: not null, Id: not null }).Select(x =>
                {
                    var responseItem = x.Source;
                    responseItem.Id = x.Id;
                    return responseItem;
                }
            ).ToList();
        }

        public async Task<object?> PrefixQueryAsync<T>(string indexName, string fieldName, string prefix) where T : IEntity
        {
            var response = await _client.SearchAsync<T>(s => s
                .Index(indexName)
                .Query(q => q
                    .Prefix(p => p
                        .Field(fieldName)
                        .Value(prefix)
                    )
                )
            );

            if (response is not { IsValidResponse: true }) return null;
            return response.Hits.Where(y => y is { Source: not null, Id: not null }).Select(x =>
                {
                    var responseItem = x.Source;
                    responseItem.Id = x.Id;
                    return responseItem;
                }
            ).ToList();
        }

        public async Task<object?> NumberRangeQueryAsync<T>(string indexName, string fieldName, double? from, double? to) where T : IEntity
        {
            var response = await _client.SearchAsync<T>(s => s
                .Index(indexName)
                .Query(q => q
                    .Range(r => r
                        .NumberRange(nr => nr
                            .Field(fieldName)
                            .Gt(from)
                            .Lt(to)
                        )
                    )
                )
            );

            if (response is not { IsValidResponse: true }) return null;
            return response.Hits.Where(y => y is { Source: not null, Id: not null }).Select(x =>
                {
                    var responseItem = x.Source;
                    responseItem.Id = x.Id;
                    return responseItem;
                }
            ).ToList();
        }

        public async Task<object?> DateRangeQueryAsync<T>(string indexName, string fieldName, DateTime? from, DateTime? to) where T : IEntity
        {
            var response = await _client.SearchAsync<T>(s => s
                .Index(indexName)
                .Query(q => q
                    .Range(r => r
                        .DateRange(nr => nr
                            .Field(fieldName)
                            .Gt(from)
                            .Lt(to)
                        )
                    )
                )
            );

            if (response is not { IsValidResponse: true }) return null;
            return response.Hits.Where(y => y is { Source: not null, Id: not null }).Select(x =>
                {
                    var responseItem = x.Source;
                    responseItem.Id = x.Id;
                    return responseItem;
                }
            ).ToList();
        }

        public async Task<object?> WildcardQueryAsync<T>(string indexName, string fieldName, string wildcardPattern) where T : IEntity
        {
            var response = await _client.SearchAsync<T>(s => s
                .Index(indexName)
                .Query(q => q
                    .Wildcard(w => w
                        .Field(fieldName)
                        .Value(wildcardPattern)
                    )
                )
            );

            if (response is not { IsValidResponse: true }) return null;
            return response.Hits.Where(y => y is { Source: not null, Id: not null }).Select(x =>
                {
                    var responseItem = x.Source;
                    responseItem.Id = x.Id;
                    return responseItem;
                }
            ).ToList();
        }

        public async Task<IEnumerable<T>?> FuzzyQueryAsync<T>(string indexName, string fieldName, string value, int? fuzziness) where T : IEntity
        {
            var response = await _client.SearchAsync<T>(s => s
                .Index(indexName)
                .Query(q => q
                    .Fuzzy(f => f
                            .Field(fieldName)
                            .Value(value)
                            .Fuzziness(fuzziness != null ? new Fuzziness($"{fuzziness.ToString()}") : new Fuzziness("Auto"))
                    )
                )
            );

            if (response is not { IsValidResponse: true }) return null;
            return response.Hits.Where(y => y is { Source: not null, Id: not null }).Select(x =>
                {
                    var responseItem = x.Source;
                    responseItem.Id = x.Id;
                    return responseItem;
                }
            ).ToList();
        }

        public async Task<object?> MatchQueryAsync<T>(string indexName, string fieldName, string value) where T : IEntity
        {
            var response = await _client.SearchAsync<T>(s => s
                .Index(indexName)
                .Query(q => q
                    .Match(t => t
                        .Field(fieldName)
                        .Query(value)
                    )
                )
            );

            if (response is not { IsValidResponse: true }) return null;
            return response.Hits.Where(y => y is { Source: not null, Id: not null }).Select(x =>
                {
                    var responseItem = x.Source;
                    responseItem.Id = x.Id;
                    return responseItem;
                }
            ).ToList();
        }

        public async Task<IEnumerable<T>?> MatchBoolPrefixQueryAsync<T>(string indexName, string fieldName, string query) where T : IEntity
        {
            var response = await _client.SearchAsync<T>(s => s
                .Index(indexName)
                .Query(q => q
                    .MatchBoolPrefix(mbp => mbp
                            .Field(fieldName)
                            .Query(query) // Aramak istediğiniz iki kelime veya daha fazlası
                    )
                )
            );

            if (response is not { IsValidResponse: true }) return null;
            return response.Hits.Where(y => y is { Source: not null, Id: not null }).Select(x =>
                {
                    var responseItem = x.Source;
                    responseItem.Id = x.Id;
                    return responseItem;
                }
            ).ToList();
        }
        public async Task<IEnumerable<T>?> MatchPhraseQueryAsync<T>(string indexName, string fieldName, string phrase) where T : IEntity
        {
            var response = await _client.SearchAsync<T>(s => s
                .Index(indexName)
                .Query(q => q
                    .MatchPhrase(mp => mp
                            .Field(fieldName)
                            .Query(phrase) // Aramak istediğiniz kelime öbeği
                    )
                )
            );

            if (response is not { IsValidResponse: true }) return null;
            return response.Hits.Where(y => y is { Source: not null, Id: not null }).Select(x =>
                {
                    var responseItem = x.Source;
                    responseItem.Id = x.Id;
                    return responseItem;
                }
            ).ToList();
        }

        public async Task<IEnumerable<T>?> CompoundQueryAsync<T>(string indexName,string mustFieldName, string mustQuery, string shouldFieldName, string shouldQuery, string mustNotFieldName, string mustNotValue,string filterFieldName, string filterFrom, string filterTo) where T : IEntity
        {
            var response = await _client.SearchAsync<T>(s => s
                .Index(indexName)
                .Query(q => q
                    .Bool(b => b
                        .Must(m => m
                            .Match(ma => ma
                                    .Field(mustFieldName)
                                    .Query(mustQuery) // İlk sorgu terimi
                            )
                        )
                        .Should(s => s
                            .Match(ma => ma
                                    .Field(shouldFieldName)
                                    .Query(shouldQuery) // İkinci sorgu terimi
                            )
                        )
                        .MustNot(mn => mn
                            .Term(t => t
                                    .Field(mustNotFieldName)
                                    .Value(mustNotValue) // Hariç tutulacak terim
                            )
                        )
                        .Filter(f => f
                            .Range(r => r
                                .DateRange(nr => nr
                                    .Field(filterFieldName)
                                    .Gt(filterFrom)
                                    .Lt(filterTo)
                                )
                            )
                        )
                    )
                )
            );

            if (response is not { IsValidResponse: true }) return null;
            return response.Hits.Where(y => y is { Source: not null, Id: not null }).Select(x =>
                {
                    var responseItem = x.Source;
                    responseItem.Id = x.Id;
                    return responseItem;
                }
            ).ToList();
        }
        public async Task<IEnumerable<T>?> MultiMatchQueryAsync<T>(string indexName, string[] fields, string query) where T : IEntity
        {
            var response = await _client.SearchAsync<T>(s => s
                .Index(indexName)
                .Query(q => q
                    .MultiMatch(mm => mm
                            .Fields(fields) 
                            .Query(query)  
                    )
                )
            );

            if (response is not { IsValidResponse: true }) return null;
            return response.Hits.Where(y => y is { Source: not null, Id: not null }).Select(x =>
                {
                    var responseItem = x.Source;
                    responseItem.Id = x.Id;
                    return responseItem;
                }
            ).ToList();
        }
    }
}

