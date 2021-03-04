using ELL.Desktop.UI.Models;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELL.Desktop.UI.Services.Lessons
{
    public class LessonsCacheDecorator : ILessonService
    {
        private readonly ILessonService inner;
        private readonly IMemoryCache pathCache;

        public LessonsCacheDecorator(ILessonService inner, IMemoryCache pathCache)
        {
            this.inner = inner;
            this.pathCache = pathCache;
        }
        public async Task<List<Lesson>> GetLessons(Guid courseId)
        {
            if (pathCache.TryGetValue<List<Lesson>>(courseId, out var data))
            {
                return data;
            }
            data = await inner.GetLessons(courseId);
            pathCache.Set(courseId, data);
            return data;
        }
    }
}
