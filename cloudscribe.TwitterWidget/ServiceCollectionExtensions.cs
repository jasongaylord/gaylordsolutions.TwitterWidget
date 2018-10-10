using cloudscribe.TwitterWidget;
using cloudscribe.TwitterWidget.Models;
using cloudscribe.TwitterWidget.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCloudScribeLatestTweets(this IServiceCollection services, IConfiguration configuration = null)
        {
            if (configuration != null)
                services.Configure<TwitterOptions>(configuration);
            else
                services.TryAddSingleton<TwitterOptions, TwitterOptions>();

            services.TryAddSingleton<TwitterCache>();
            services.TryAddScoped<ITwitterService, TwitterService>();

            return services;
        }
    }
}
