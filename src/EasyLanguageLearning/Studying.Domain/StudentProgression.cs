using Courses.Domain;
using EasyLanguageLearning.Domain.Shared.Kernel.Units;
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
        internal List<CourseProgression> History { get; private set; }

        public StudentProgression(Student student)
        {
            Student = student;
            CurrentCourse = CourseId.Empty;
            CurrentUnit = UnitId.Empty;
        }

        internal void StartCourse(CourseId course, List<UnitId> units, List<UnitContentItemId> unitContentList)
        {
            if(CurrentCourse != CourseId.Empty)
            {
                throw new ArgumentException($"{nameof(course)} already started");
            }
            CurrentCourse = course;
            CurrentUnit = units.First();
            History = new List<CourseProgression>
            {
                new CourseProgression(course, 
                    ToUnitProgression(units, unitContentList))
            };
        }

        //Todo
        private List<UnitProgression> ToUnitProgression(List<UnitId> units, List<UnitContentItemId> unitContentList) =>
            units
                .Select(u => ToUnitProgression(u, new List<UnitContentItemId>()))
                .ToList();

        private UnitProgression ToUnitProgression(UnitId unitId, List<UnitContentItemId> unitContentList) =>
            new UnitProgression(unitId, unitContentList);

        internal int GetCompletionPercentage()
        {
            var currProgression = History.First(h => h.Course == CurrentCourse);
            var percentage = currProgression.GetCompletionPercentaje();
            //todo roundUp
            int result = (int)percentage;
            return result;

        }
    }
}
