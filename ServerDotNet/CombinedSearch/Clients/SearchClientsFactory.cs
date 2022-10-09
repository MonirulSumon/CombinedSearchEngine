namespace CombinedSearch.Clients
{
    public class SearchClientsFactory : ISearchClientFactory
    {
        private readonly ISearchClient _googleClient;
        private readonly ISearchClient _bingClient;
        private readonly ISearchClient _wikiClient;

        public SearchClientsFactory(BingClient bingClient, GoogleClient googleClient, WikiClient wikiClient)
        {
            _bingClient = bingClient;
            _googleClient = googleClient;
            _wikiClient = wikiClient;
        }

        public ISearchClient GoogleClient => _googleClient;
        public ISearchClient BingClient => _bingClient;
        public ISearchClient WikiClient => _wikiClient;
    }

    public interface ISearchClientFactory
    {
        ISearchClient GoogleClient { get; }
        ISearchClient BingClient { get; }
        ISearchClient WikiClient { get; }
    }
}
