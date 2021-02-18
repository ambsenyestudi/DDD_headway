using System.Collections.Generic;
using System.Threading.Tasks;

namespace Courses.Domain
{
    public interface ICoursesRepository
    {
        Task<IEnumerable<Course>> GetAll();
    }
}
