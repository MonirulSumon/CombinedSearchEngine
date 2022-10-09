namespace CombinedSearch.Utils
{
    public class SearchEngineConfig
    {
        public GoogleSearchConfig GoogleSearchConfig { get; set; }
        public BingSearchConfig BingSearchConfig { get; set; }
        public WikiSearchConfig WikiSearchConfig { get; set; }
    }

    public class GoogleSearchConfig
    {
        public string Host { get; set; }
        public string ApiKey { get; set; }
        public string SearchEngineContext { get; set; }
    }

    public class BingSearchConfig
    {
        public string Host { get; set; }
        public string ApiKey { get; set; }
        public string CustomConfig { get; set; }
        public string HeaderKeyName { get; set; }
    }
    public class WikiSearchConfig
    {
        public string Host { get; set; }
    }
}
