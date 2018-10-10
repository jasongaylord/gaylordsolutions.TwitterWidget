using cloudscribe.TwitterWidget.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace cloudscribe.TwitterWidget
{
    public interface ITwitterService
    {
        string ObtainBearerToken(string key, string secret);
        Task<List<TweetStruct>> RetrieveTweets(TwitterOptions options, string key);
    }
}
