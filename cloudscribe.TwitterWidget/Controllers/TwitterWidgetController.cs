using cloudscribe.TwitterWidget.Models;
using cloudscribe.TwitterWidget.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace cloudscribe.TwitterWidget.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class TwitterWidgetController : Controller
    {
        protected ITwitterService TwitterService { get; private set; }
        protected TwitterOptions TwitterOptions { get; private set; }
        protected ILogger Log { get; private set; }
        protected TwitterCache Cache { get; private set; }

        public TwitterWidgetController(ITwitterService twitterService, ILogger<TwitterWidgetController> logger, TwitterCache cache, IOptions<TwitterOptions> options = null)
        {
            TwitterService = twitterService;
            Log = logger;
            Cache = cache;

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
            var twitter = new TwitterCacheWrapperService(TwitterService, Log, Cache, TwitterOptions);
            var result = await twitter.RetrieveCachedTweetsAsync();

            return result.IsSuccessful ? new JsonResult(result.Tweets) : new JsonResult(Globals.JsonErrorMessage);
        }
    }
}
