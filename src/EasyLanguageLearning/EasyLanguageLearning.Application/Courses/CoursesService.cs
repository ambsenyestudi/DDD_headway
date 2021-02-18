using Courses.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyLanguageLearning.Application.Courses
{
    public class CoursesService : ICoursesService
    {
        private readonly ICoursesRepository courseRepository;

        public CoursesService(ICoursesRepository courseRepository)
        {
            this.courseRepository = courseRepository;
        }
        public Task<IEnumerable<CourseDTO>> GetCourses() =>
            courseRepository.GetAll();
        
            
    }
}
