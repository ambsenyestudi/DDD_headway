using ELL.Desktop.UI.Models;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ELL.Desktop.UI.Services.Paths
{
    public class PathCacheDecorator : IPathService
    {
        private const string pathCacheKey = "leaningPathList";
        private readonly IPathService inner;
        private readonly IMemoryCache pathCache;

        public PathCacheDecorator(IPathService inner, IMemoryCache pathCache)
        {
            this.inner = inner;
            this.pathCache = pathCache;
        }
        public async Task<List<LearningPath>> GetPaths()
        {
            if (pathCache.TryGetValue<List<LearningPath>>(pathCacheKey, out var data))
            {
                return data;
            }
            data = await inner.GetPaths();
            pathCache.Set(pathCacheKey, data);
            return data;
        }
    }
}
