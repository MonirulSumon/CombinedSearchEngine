using CombinedSearch.Clients;
using CombinedSearch.Services;
using Microsoft.AspNetCore.HttpLogging;

namespace CombinedSearch.Startup
{
    public static class DependencyInjectionSetup
    {
        public static IServiceCollection RegisterApplicationServices(this IServiceCollection services)
        {
            services.AddHttpClient<GoogleClient>();

            services.AddHttpClient<BingClient>();

            services.AddHttpClient<WikiClient>();

            services.AddScoped<ISearchClientFactory, SearchClientsFactory>();

            services.AddScoped<ISearchService, SearchService>();

            return services;
        }
        public static IServiceCollection RegisterLoggerServices(this IServiceCollection services)
        {

            services.AddHttpLogging(logging =>
            {
                logging.LoggingFields = HttpLoggingFields.ResponsePropertiesAndHeaders |
                                        HttpLoggingFields.RequestPath |
                                        HttpLoggingFields.ResponseStatusCode |
                                        HttpLoggingFields.ResponseBody;
            });

            services.AddLogging(opt =>
            {
                opt.AddSimpleConsole(c =>
                {
                    c.TimestampFormat = "[yyyy/MM/dd hh:mm:ss] ";
                });
            });

            return services;
        }
    }
}
