using cloudscribe.TwitterWidget.Models;
using cloudscribe.TwitterWidget.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace cloudscribe.TwitterWidget.ViewComponents
{
    public class RetrieveTweetsViewComponent : ViewComponent
    {
        protected ITwitterService TwitterService { get; private set; }
        protected TwitterOptions TwitterOptions { get; private set; }
        protected ILogger Log { get; private set; }
        protected TwitterCache Cache { get; private set; }

        public RetrieveTweetsViewComponent(ITwitterService twitterService, ILogger<RetrieveTweetsViewComponent> logger, TwitterCache cache, IOptions<TwitterOptions> options = null)
        {
            TwitterService = twitterService;
            Log = logger;
            Cache = cache;

            if (options != null)
                TwitterOptions = options.Value;
            else
                TwitterOptions = new TwitterOptions();
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            //TwitterOptions.Count = numberToShow;
            var twitter = new TwitterCacheWrapperService(TwitterService, Log, Cache, TwitterOptions);
            var result = await twitter.RetrieveCachedTweetsAsync();

            return View("Default", result);
        }
    }
}