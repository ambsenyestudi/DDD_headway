using Courses.Domain;

namespace Studying.Domain
{
    public class StudentProgressionAggregate
    {
        private ICouserLookup couserLookup;

        public StudentProgressionAggregate(ICouserLookup couserLookup)
        {
            this.couserLookup = couserLookup;
        }
        public StudentProgression CreateProgression(Student student)
        {
            return new StudentProgression(student);
        }
        public StudentProgression StartCourse(StudentProgression progression, CourseId course)
        {
            progression.StartCourse(course, couserLookup);
            return progression;
        }
    }
}
