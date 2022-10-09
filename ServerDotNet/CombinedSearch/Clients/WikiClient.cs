using Microsoft.Extensions.Options;
using CombinedSearch.Model;
using CombinedSearch.ResponseModel;
using CombinedSearch.Utils;
using System.Text.Json;

namespace CombinedSearch.Clients
{
    public class WikiClient : ISearchClient
    {
        private readonly HttpClient _client;
        private readonly WikiSearchConfig _wikiConfig;
        private readonly JsonSerializerOptions _serializerOptions;

        public WikiClient(HttpClient client, IOptions<SearchEngineConfig> configOptions)
        {
            _client = client;
            _wikiConfig = configOptions.Value.WikiSearchConfig;

            _client.BaseAddress = new Uri(_wikiConfig.Host);
            _client.DefaultRequestHeaders.Clear();

            _serializerOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public async Task<ISearchResponse> GetDataAsync(string query)
        {
            var requestUri = $"api.php?action=query&list=search&prop=info&inprop=url&utf8=&format=json&origin=*&srsearch={query}";
            var response = await _client.GetAsync(requestUri, HttpCompletionOption.ResponseHeadersRead);

            response.EnsureSuccessStatusCode();

            var stream = await response.Content.ReadAsStreamAsync();

            var wikiSearchResult = await JsonSerializer.DeserializeAsync<WikiSearchResponse>(stream, _serializerOptions);
            if (wikiSearchResult is not null)
            {
                wikiSearchResult.Query.Searchinfo.Param = query;
            }

            return wikiSearchResult ?? new WikiSearchResponse();
        }
    }
}
