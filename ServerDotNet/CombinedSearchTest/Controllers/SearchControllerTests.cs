using CombinedSearch.Controllers;
using CombinedSearch.DtoModel;
using CombinedSearch.Services;
using FluentAssertions;
using FluentAssertions.Execution;
using Moq;
using NUnit.Framework;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace CombinedSearch.Test.Controllers
{
    public class SearchControllerTests
    {
        private readonly Mock<ISearchService> _searchService;
        private readonly SearchController searchController;

        public SearchControllerTests()
        {
            _searchService = new Mock<ISearchService>();
            searchController = new SearchController(_searchService.Object);
        }

        [Test]
        public void AvailableSearchEnginesShould_ReturnListOfStringAndSize3()
        {
            var result = searchController.AvailableSearchEngines();
            using (new AssertionScope())
            {
                result.Should().BeOfType<List<string>>();
                result.Count.Should().NotBe(1);
                result.Count.Should().Be(3);
            };
        }

        [Test]
        public async Task GoogleSearchAsyncShould_ReturnTypeOfTotalSearchResult()
        {
            var mokeReturnData = new TotalSearchResult("Helloo", 1000)
            {
                QueryWordResults = new List<SearchResult>() { new SearchResult("Helloo", 1000) }

            };
            _searchService.Setup(x => x.GoogleSearchDataAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(mokeReturnData));

            var result = await searchController.GoogleSearchAsync("hello");

            result.Should().BeOfType<TotalSearchResult>();
            result.FormattedTotalHits.Should().Be("1K");
            result.QueryWordResults.Should().HaveCount(1);
        }

    }
}
