using CombinedSearch.Clients;
using CombinedSearch.DtoModel;
using CombinedSearch.Model;

namespace CombinedSearch.Services
{
    public class SearchService : ISearchService
    {
        private readonly ISearchClientFactory _clientFactory;

        public SearchService(ISearchClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<TotalSearchResult> GoogleSearchDataAsync(string query)
        {
            var responses = await Task.WhenAll(GetTasks(query, SearchEngineType.GOOGLE));
            var numberOfHits = responses.Sum(x => x.GetHitCounts().Hits);
            var result = new TotalSearchResult(query, numberOfHits);
            result.QueryWordResults = responses.Select(x => new SearchResult(x.GetHitCounts().SearchQuery, x.GetHitCounts().Hits)).ToList();

            return result;
        }

        public async Task<TotalSearchResult> BingSearchDataAsync(string query)
        {
            var responseList = await Task.WhenAll(GetTasks(query, SearchEngineType.BING));
            var numberOfHits = responseList.Sum(x => x.GetHitCounts().Hits);
            var result = new TotalSearchResult(query, numberOfHits);
            result.QueryWordResults = responseList.Select(x => new SearchResult(x.GetHitCounts().SearchQuery, x.GetHitCounts().Hits)).ToList();

            return result;
        }

        public async Task<TotalSearchResult> WikiSearchDataAsync(string query)
        {
            var responseList = await Task.WhenAll(GetTasks(query, SearchEngineType.WIKI));
            var numberOfHits = responseList.Sum(x => x.GetHitCounts().Hits);
            var result = new TotalSearchResult(query, numberOfHits)
            {
                QueryWordResults = responseList.Select(x => new SearchResult(x.GetHitCounts().SearchQuery, x.GetHitCounts().Hits)).ToList()
            };

            return result;
        }

        private IEnumerable<Task<ISearchResponse>> GetTasks(string query, SearchEngineType engineType)
        {
            var queries = query.Split(" ");
            IEnumerable<Task<ISearchResponse>> searchTasks = new List<Task<ISearchResponse>>();

            switch (engineType)
            {
                case SearchEngineType.GOOGLE:
                    searchTasks = queries.Select(x => _clientFactory.GoogleClient.GetDataAsync(x));
                    break;
                case SearchEngineType.BING:
                    searchTasks = queries.Select(x => _clientFactory.BingClient.GetDataAsync(x));
                    break;
                case SearchEngineType.WIKI:
                    searchTasks = queries.Select(x => _clientFactory.WikiClient.GetDataAsync(x));
                    break;
            }
            return searchTasks;
        }
    }

    public interface ISearchService
    {
        Task<TotalSearchResult> GoogleSearchDataAsync(string query);
        Task<TotalSearchResult> BingSearchDataAsync(string query);
        Task<TotalSearchResult> WikiSearchDataAsync(string query);
    }
}
