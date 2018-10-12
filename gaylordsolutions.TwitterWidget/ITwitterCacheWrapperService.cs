using gaylordsolutions.TwitterWidget.Models;
using System.Threading.Tasks;

namespace gaylordsolutions.TwitterWidget
{
    public interface ITwitterCacheWrapperService
    {
        Task<RetrieveTweetsResult> RetrieveCachedTweetsAsync();
    }
}