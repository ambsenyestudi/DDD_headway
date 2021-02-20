using System;

namespace EasyLanguageLearning.Domain.ContentSupplying.Aggregate
{
    public class ContentSupplyingAggreate
    {
        public LearningPath CareteLearningPath(Guid id, string name)
        {
            var path = new LearningPath(id);
            path.UpdatName(name);
            return path;
        }
        public Course CreateCourseFromPath(Guid id, LearningPath path, int level = 0)
        {
            var courseName = Course.NameFromLearningPath(path, level);

            return new Course(id, path.Id, courseName);
        }
        
    }
}
