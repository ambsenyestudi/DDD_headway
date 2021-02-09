using Courses.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Studying.Domain
{
    public class StudentProgression
    {
        public Student Student { get; }
        public CourseId CurrentCourse { get; private set; }
        public UnitId CurrentUnit { get; private set; }
        public int CompletionPercentage { get; set; }

        public StudentProgression(Student student)
        {
            Student = student;
            CurrentCourse = CourseId.Empty;
            CurrentUnit = UnitId.Empty;
        }

        internal void StartCourse(CourseId course, List<UnitId> units)
        {
            if(CurrentCourse != CourseId.Empty)
            {
                throw new ArgumentException($"{nameof(course)} already started");
            }
            CurrentCourse = course;
            CurrentUnit = units.First();
            CompletionPercentage = 0;
        }
    }
}
