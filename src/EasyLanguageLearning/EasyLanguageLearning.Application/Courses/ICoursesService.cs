using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EasyLanguageLearning.Application.Courses
{
    public interface ICoursesService
    {
        public Task<IEnumerable<CourseDTO>> GetCourses();
    }
}
