using Microsoft.Extensions.Caching.Memory;

namespace gaylordsolutions.TwitterWidget.Services
{
    public class TwitterCache
    {
        public MemoryCache Cache { get; set; }
        public TwitterCache()
        {
            Cache = new MemoryCache(new MemoryCacheOptions());
        }
    }
}