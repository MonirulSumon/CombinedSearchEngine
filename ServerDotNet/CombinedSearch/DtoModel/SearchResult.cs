
using CombinedSearch.Utils;

namespace CombinedSearch.DtoModel
{
    public class SearchResult
    {
        public SearchResult(string searchQuery, long hits)
        {
            SearchQuery = searchQuery;
            Hits = hits;
        }
        public string SearchQuery { get; set; } = string.Empty;
        public long Hits { get; set; } = 0;
        public string FormattedTotalHits => Hits.ToNumericFormat();
    }
    public class TotalSearchResult : SearchResult
    {
        public TotalSearchResult(string searchQuery, long hits) : base(searchQuery, hits)
        {
        }
        public IEnumerable<SearchResult> QueryWordResults { get; set; }
    }

}
