using cloudscribe.TwitterWidget.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace cloudscribe.TwitterWidget
{
    public interface ITwitterService
    {
        Task<List<TweetStruct>> RetrieveTweets();
    }
}
