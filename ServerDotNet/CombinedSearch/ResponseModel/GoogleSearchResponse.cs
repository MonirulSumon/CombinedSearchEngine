using CombinedSearch.DtoModel;
using CombinedSearch.Utils;

namespace CombinedSearch.Model
{
    public class GoogleSearchResponse : ISearchResponse
    {
        public Queries Queries { get; set; }
        public Searchinformation SearchInformation { get; set; }

        public SearchResult GetHitCounts()
        {
            var request = Queries.Request.FirstOrDefault();
            var searchQuesry = request is null ? string.Empty : request.SearchTerms;
            var hits = SearchInformation.TotalHits;

            return new SearchResult(searchQuesry, hits);
        }
    }

    public class Queries
    {
        public Request[] Request { get; set; }
    }

    public class Request
    {
        public string Title { get; set; }
        public string TotalResults { get; set; }
        public string SearchTerms { get; set; }
        public int Count { get; set; }
    }

    public class Searchinformation
    {
        public float SearchTime { get; set; }
        public string FormattedSearchTime { get; set; }
        public string TotalResults { get; set; }
        public long TotalHits => long.TryParse(TotalResults, out long hits) ? hits : 0;
        public string FormattedTotalResults { get; set; }
        public string NumericFormattedTotalHits => TotalHits.ToNumericFormat();
    }
}