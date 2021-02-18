using Courses.Domain;
using EasyLanguageLearning.Domain.Shared.Kernel;
using EasyLanguageLearning.Domain.Shared.Kernel.Units;
using System;
using System.Collections.Generic;

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

        public int GetCompletionPercentage(StudentProgression sut) =>
            sut.GetCompletionPercentage();

        private StudentProgression CreateProgression(Student student) =>
            new StudentProgression(student);

        private StudentProgression StartCourse(StudentProgression studentProgression, CourseId course)
        {
            var currentCourseUnits = courseLookup.GetUnits(course);
            //todo get unit items
            studentProgression.StartCourse(course, currentCourseUnits, new List<UnitContentItemId>());
            return studentProgression;
        }

    }
}
