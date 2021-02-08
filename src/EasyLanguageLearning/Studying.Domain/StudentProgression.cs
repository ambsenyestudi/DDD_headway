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
        public List<UnitId> UnitList { get; private set; }
        public UnitId CurrentUnit { get; private set; }
        private List<UnitProgression> UnitProgressionsList { get; } = new List<UnitProgression>();
        public StudentProgression(Student student)
        {
            Student = student;
            CurrentCourse = CourseId.Empty;
            CurrentUnit = UnitId.Empty;
        }

        internal void StartCourse(CourseId course, ICouserLookup couserLookup)
        {
            if(CurrentCourse != CourseId.Empty)
            {
                throw new ArgumentException($"{nameof(course)} already started");
            }
            CurrentCourse = course;
            UnitList = couserLookup.GetUnits(CurrentCourse);
            CurrentUnit = UnitList.First();
        }
    }
}
