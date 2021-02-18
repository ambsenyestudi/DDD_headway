using Courses.Domain;
using EasyLanguageLearning.Domain.Shared.Kernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Studying.Domain
{
    public class LearningPathProgression
    {
        public LearningPathDefinition LearningPath { get; }
        private List<CourseProgression> CoursesList { get; } = new List<CourseProgression>();
        public LearningPathProgression(LearningPathDefinition learningPathDefinition)
        {
            LearningPath = learningPathDefinition;
        }
        public void StartCourse(CourseId course, List<UnitId> unitList )
        {

        }
    }
}
