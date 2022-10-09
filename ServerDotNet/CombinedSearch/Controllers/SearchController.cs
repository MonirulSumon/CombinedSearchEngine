using Microsoft.AspNetCore.Mvc;
using CombinedSearch.DtoModel;
using CombinedSearch.Model;
using CombinedSearch.Services;
using System.ComponentModel.DataAnnotations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CombinedSearch.Controllers
{
    [Route("api/[controller]/[action]")]
    [Produces("application/json")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly ISearchService _searchService;

        public SearchController(ISearchService searchService)
        {
            _searchService = searchService;
        }


        [HttpGet]
        public List<string> AvailableSearchEngines()
        {
            var searchResult = Enum.GetNames(typeof(SearchEngineType)).ToList();

            return searchResult;
        }

        [HttpGet]
        public async Task<TotalSearchResult> AllSearchAsync([Required][FromQuery] string query)
        {
            var searchTasks = new List<Task<TotalSearchResult>>
            {
                _searchService.GoogleSearchDataAsync(query),
                _searchService.BingSearchDataAsync(query),
                _searchService.WikiSearchDataAsync(query)
            };
            var result = await Task.WhenAll(searchTasks);

            var individualKeyResult = result.SelectMany(x => x.QueryWordResults)
                                            .GroupBy(x => x.SearchQuery)
                                            .Select(h => new SearchResult(h.First().SearchQuery, h.Sum(c => c.Hits))).ToList();

            var totalSearchResult = new TotalSearchResult(query, result.Sum(x => x.Hits))
            {
                QueryWordResults = individualKeyResult
            };

            return totalSearchResult;
        }

        [HttpGet]
        public async Task<TotalSearchResult> GoogleSearchAsync([Required][FromQuery] string query)
        {
            return await _searchService.GoogleSearchDataAsync(query);
        }

        [HttpGet]
        public async Task<TotalSearchResult> BingSearchAsync([Required][FromQuery] string query)
        {
            return await _searchService.BingSearchDataAsync(query);
        }

        [HttpGet]
        public async Task<TotalSearchResult> WikiSearchAsync([Required][FromQuery] string query)
        {
            return await _searchService.WikiSearchDataAsync(query);
        }
    }
}
