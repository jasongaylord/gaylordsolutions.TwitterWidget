using cloudscribe.TwitterWidget.Models;
using System.Threading.Tasks;

namespace cloudscribe.TwitterWidget
{
    public interface ITwitterCacheWrapperService
    {
        Task<RetrieveTweetsResult> RetrieveCachedTweetsAsync();
    }
}