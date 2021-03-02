using ELL.Desktop.UI.Models;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ELL.Desktop.UI.Services.Courses
{
    public class CourseCacheDecorator : ICourseService
    {
        private readonly ICourseService inner;
        private readonly IMemoryCache cache;

        public CourseCacheDecorator(ICourseService inner, IMemoryCache cache)
        {
            this.inner = inner;
            this.cache = cache;
        }
        public async Task<List<Course>> GetCourses(Guid courseId)
        {
            if (cache.TryGetValue<List<Course>>(courseId, out var data))
            {
                return data;
            }
            data = await inner.GetCourses(courseId);
            cache.Set(courseId, data);
            return data;
        }
    }
}
