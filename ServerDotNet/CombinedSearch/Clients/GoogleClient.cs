using Microsoft.Extensions.Options;
using CombinedSearch.Model;
using CombinedSearch.Utils;
using System.Text.Json;

namespace CombinedSearch.Clients
{
    public class GoogleClient : ISearchClient
    {
        private readonly HttpClient _client;
        private readonly GoogleSearchConfig _googleConfig;
        private readonly JsonSerializerOptions _serializerOptions;

        public GoogleClient(HttpClient client, IOptions<SearchEngineConfig> configOptions)
        {
            _client = client;
            _googleConfig = configOptions.Value.GoogleSearchConfig;

            _client.BaseAddress = new Uri(_googleConfig.Host);
            _client.DefaultRequestHeaders.Clear();

            _serializerOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public async Task<ISearchResponse> GetDataAsync(string query)
        {
            var requestUri = $"v1?key={_googleConfig.ApiKey}&cx={_googleConfig.SearchEngineContext}&q={query}";
            var response = await _client.GetAsync(requestUri, HttpCompletionOption.ResponseHeadersRead);

            response.EnsureSuccessStatusCode();

            var stream = await response.Content.ReadAsStreamAsync();

            var googleSearchResult = await JsonSerializer.DeserializeAsync<GoogleSearchResponse>(stream, _serializerOptions);

            return googleSearchResult ?? new GoogleSearchResponse();
        }
    }
}
