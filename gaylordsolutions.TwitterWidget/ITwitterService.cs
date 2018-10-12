using cloudscribe.TwitterWidget.Models;
using System.Threading.Tasks;

namespace cloudscribe.TwitterWidget
{
    public interface ITwitterService
    {
        string ObtainBearerToken(string key, string secret);
        Task<RetrieveTweetsResult> RetrieveTweetsAsync(TwitterOptions options, string key);
    }
}
