using ELL.Desktop.UI.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ELL.Desktop.UI.Services.Lessons
{
    public interface ILessonService
    {
        Task<List<Lesson>> GetLessons(Guid courseId);
    }
}
