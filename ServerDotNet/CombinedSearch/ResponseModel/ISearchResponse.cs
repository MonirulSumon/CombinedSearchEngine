using CombinedSearch.DtoModel;

namespace CombinedSearch.Model
{
    public interface ISearchResponse
    {
        SearchResult GetHitCounts();
    }
}