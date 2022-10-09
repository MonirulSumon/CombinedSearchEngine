using CombinedSearch.Model;

namespace CombinedSearch.Clients
{
    public interface ISearchClient
    {
        Task<ISearchResponse> GetDataAsync(string query);
    }

}
