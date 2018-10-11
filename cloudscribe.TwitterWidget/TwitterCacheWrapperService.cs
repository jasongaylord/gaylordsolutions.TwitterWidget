using System;
using System.Threading.Tasks;
using cloudscribe.TwitterWidget.Models;
using cloudscribe.TwitterWidget.Services;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace cloudscribe.TwitterWidget
{
    public class TwitterCacheWrapperService : ITwitterCacheWrapperService
    {
        private ITwitterService TwitterService { get; set; }
        private TwitterOptions TwitterOptions { get; set; }
        private ILogger Log { get; set; }
        private MemoryCache _cache { get; set; }

        public TwitterCacheWrapperService(ITwitterService twitterService, ILogger logger, TwitterCache cache, TwitterOptions options = null)
        {
            TwitterService = twitterService;
            Log = logger;
            _cache = cache.Cache;

            if (options != null)
                TwitterOptions = options;
            else
                TwitterOptions = new TwitterOptions();
        }

        public async Task<RetrieveTweetsResult> RetrieveCachedTweetsAsync()
        {
            var key = Globals.CacheKey + TwitterOptions.Username + "_RetrieveTweets";
            var result = new RetrieveTweetsResult();

            try
            {
                if (!_cache.TryGetValue(key, out result))
                {
                    result = await TwitterService.RetrieveTweetsAsync(TwitterOptions, key);
                    _cache.Set(key, result, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(TwitterOptions.CacheMinutes)));
                }
            }
            catch (Exception ex)
            {
                result.Exception = ex;
                Log.LogError(ex, "Error occurred in RetrieveTweets().");
            }

            return result;
        }
    }
}