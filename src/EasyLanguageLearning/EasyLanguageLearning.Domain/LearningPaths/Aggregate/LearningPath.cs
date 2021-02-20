﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace EasyLanguageLearning.Domain.LearningPaths.Aggregate
{
    public class LearningPath
    {
        public LearningPathId Id { get; protected set; }
        public string Name { get; protected set; }
        public ICollection<Course> Courses { get; private set; } = new List<Course>();
        //maby override collections to set pathName
        protected LearningPath()
        {
        }
        public LearningPath(Guid id, string name)
        {
            Id = new LearningPathId(id);
            Name = name;
        }

        public void AddCourseFromLevel(int level, string id = "")
        {
            EnsurePositiveLevel(level);
            EnsureCourseLevelNotRepeated(level);

            if (!Guid.TryParse(id, out Guid courseId))
            {
                courseId = Guid.NewGuid();
            }
            var courseName = CourseName.Create(Name, level);
                        
            var newCourse = new Course(courseId, Id, courseName);
            Courses.Add(newCourse);
        }
        public void EnsureCourseLevelNotRepeated(int level) 
        {
            var currLevel = CourseLevel.Create(level);
            if (Courses.Any(c => c.Level == currLevel)) 
            {
                throw new ArgumentException("Repeated course level");
            }
        }
        public void EnsurePositiveLevel(int level)
        {
            if (!CourseLevel.IsPositiveLevel(level))
            {
                throw new ArgumentException("Invalid course settings");
            }
        }
    }
}
