using cloudscribe.TwitterWidget.Models;
using cloudscribe.TwitterWidget.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace cloudscribe.TwitterWidget.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class TwitterWidgetController : Controller
    {
        protected ITwitterService TwitterService { get; private set; }
        protected TwitterOptions TwitterOptions { get; private set; }
        protected ILogger Log { get; private set; }
        private MemoryCache _cache;
        private static readonly string CacheKey = "TwitterCache_";

        public TwitterWidgetController(ITwitterService twitterService, ILogger<TwitterWidgetController> logger, TwitterCache cache, IOptions<TwitterOptions> options = null)
        {
            TwitterService = twitterService;
            Log = logger;
            _cache = cache.Cache;

            if (options != null)
                TwitterOptions = options.Value;
            else
                TwitterOptions = new TwitterOptions();
        }

        public ActionResult Index()
        {
            return Content("Success!");
        }

        [HttpPost]
        [Route("twitter/gettweets")]
        public virtual async Task<IActionResult> RetrieveTweets()
        {
            var key = CacheKey + TwitterOptions.Username + "_RetrieveTweets";
            var results = new List<TweetStruct>();

            try
            {
                if (!_cache.TryGetValue(key, out results))
                {
                    results = await TwitterService.RetrieveTweets(TwitterOptions, key);
                    _cache.Set(key, results, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(TwitterOptions.CacheMinutes)));
                }
            }
            catch (Exception ex)
            {
                Log.LogError(ex, "Error occurred in RetrieveTweets().");
                return new JsonResult("{\"error\":\"The TwitterWidget threw an error. Check the logs for more details.\"}");
            }

            return new JsonResult(results);
        }
    }
}
