using System.Collections.Generic;
using System.Threading.Tasks;

namespace EasyLanguageLearning.Application.Courses
{
    public interface ICoursesRepository
    {
        Task<IEnumerable<CourseDTO>> GetAll();
    }
}
