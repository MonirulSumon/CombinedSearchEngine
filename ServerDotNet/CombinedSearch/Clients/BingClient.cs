using Microsoft.Extensions.Options;
using CombinedSearch.Model;
using CombinedSearch.Utils;
using System.Text.Json;

namespace CombinedSearch.Clients
{
    public class BingClient : ISearchClient
    {
        private readonly HttpClient _client;
        private readonly BingSearchConfig _config;
        private readonly JsonSerializerOptions _serializerOptions;

        public BingClient(HttpClient client, IOptions<SearchEngineConfig> configOptions)
        {
            _client = client;
            _config = configOptions.Value.BingSearchConfig;

            _client.BaseAddress = new Uri(_config.Host);
            _client.DefaultRequestHeaders.Clear();
            _client.DefaultRequestHeaders.Add(_config.HeaderKeyName, _config.ApiKey);
            _serializerOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public async Task<ISearchResponse> GetDataAsync(string query)
        {

            var requestUri = $"custom/search?customconfig={_config.CustomConfig}&q={query}&mkt=en-US";
            var response = await _client.GetAsync(requestUri, HttpCompletionOption.ResponseHeadersRead);

            try
            {
                response.EnsureSuccessStatusCode();
                var stream = await response.Content.ReadAsStreamAsync();

                return await JsonSerializer.DeserializeAsync<BingSearchResponse>(stream, _serializerOptions) ?? new BingSearchResponse();
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
    }
}
