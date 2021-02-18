using EasyLanguageLearning.Application.Courses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyLanguageLearning.Infrastructure.Courses
{
    public class CoursesRepository: ICoursesRepository
    {
        private readonly DataContext context;

        public CoursesRepository(DataContext context)
        {
            this.context = context;
        }
        public Task<IEnumerable<CourseDTO>> GetAll()
        {
            return Task.FromResult(context.Courses.AsEnumerable());
        }
            
    }
}
