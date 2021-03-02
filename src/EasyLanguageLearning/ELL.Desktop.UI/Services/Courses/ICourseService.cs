using ELL.Desktop.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELL.Desktop.UI.Services.Courses
{
    public interface ICourseService
    {
        Task<List<Course>> GetCourses(Guid couresId);
    }
}
