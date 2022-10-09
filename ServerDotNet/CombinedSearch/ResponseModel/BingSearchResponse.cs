using CombinedSearch.DtoModel;

namespace CombinedSearch.Model
{
    public class BingSearchResponse : ISearchResponse
    {
        public Querycontext QueryContext { get; set; }
        public Webpages WebPages { get; set; }

        public SearchResult GetHitCounts()
        {
            return new SearchResult(QueryContext.OriginalQuery, WebPages.TotalEstimatedMatches);
        }
    }

    public class Querycontext
    {
        public string OriginalQuery { get; set; }
    }

    public class Webpages
    {
        public string WebSearchUrl { get; set; }
        public string WebSearchUrlPingSuffix { get; set; }
        public long TotalEstimatedMatches { get; set; }
    }

}