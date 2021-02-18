using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyLanguageLearning.Domain.ContentSupplying.Aggregate
{
    public class Course
    {
        public LearningPathId LearningPathId { get; protected set; }
        public CourseId Id { get; protected set; }
        public Course(Guid id, LearningPathId learningPathId)
        {
            Id = new CourseId(id);
            LearningPathId = learningPathId;
        }
        protected Course()
        {
        }
        
    }
}
