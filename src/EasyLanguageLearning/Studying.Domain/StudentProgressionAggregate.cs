using Courses.Domain;
using EasyLanguageLearning.Domain.Shared.Kernel;
using System;

namespace Studying.Domain
{
    public class StudentProgressionAggregate
    {
        private ICourseLookup courseLookup;

        public StudentProgressionAggregate(ICourseLookup courseLookup)
        {
            this.courseLookup = courseLookup;
        }

        

        public StudentProgression StartLearningPath(Student student, LearningPathDefinition learningPath)
        {
            var studentProgression = CreateProgression(student);
            return StartLearningPath(learningPath, studentProgression);
        }
        public StudentProgression StartLearningPath(LearningPathDefinition learningPath, StudentProgression studentProgression) =>
            StartCourse(studentProgression, 
                courseLookup.GetCourse(learningPath));
        
        private StudentProgression CreateProgression(Student student) =>
            new StudentProgression(student);

        private StudentProgression StartCourse(StudentProgression studentProgression, CourseId course)
        {
            var currentCourseUnits = courseLookup.GetUnits(course);
            studentProgression.StartCourse(course, currentCourseUnits);
            return studentProgression;
        }
    }
}
