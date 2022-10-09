using CombinedSearch.DtoModel;
using CombinedSearch.Model;

namespace CombinedSearch.ResponseModel
{
    public class WikiSearchResponse : ISearchResponse
    {
        public Query Query { get; set; }

        public SearchResult GetHitCounts()
        {
            return new SearchResult(Query.Searchinfo.Param, Query.Searchinfo.Totalhits);
        }
    }
    public class Query
    {
        public Searchinfo Searchinfo { get; set; }
    }

    public class Searchinfo
    {
        public long Totalhits { get; set; }
        public string Param { get; set; } = string.Empty;
    }

}