using cloudscribe.TwitterWidget.Models;
using cloudscribe.TwitterWidget.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace cloudscribe.TwitterWidget.Controllers
{
    public class TwitterWidgetController : Controller
    {
        protected IHostingEnvironment HostingEnvironment { get; private set; }
        protected ITwitterService TwitterService { get; private set; }
        protected TwitterOptions TwitterOptions { get; private set; }
        protected ILogger Log { get; private set; }
        private MemoryCache _cache;
        private static readonly string CacheKey = "_TwitterCache";

        public TwitterWidgetController(IHostingEnvironment appEnv, ITwitterService twitterService, ILogger<TwitterWidgetController> logger, TwitterCache cache, IOptions<TwitterOptions> options = null)
        {
            HostingEnvironment = appEnv;
            TwitterService = twitterService;
            Log = logger;
            _cache = cache.Cache;

            if (options != null)
                TwitterOptions = options.Value;
            else
                TwitterOptions = new TwitterOptions();
        }

        [HttpPost]
        [Route("twitter/gettweets")]
        public virtual async Task<IActionResult> RetrieveTweets()
        {
            return new JsonResult("");
        }
    }
}
