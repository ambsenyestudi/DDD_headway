using Courses.Domain;
using Moq;
using Studying.Domain;
using System;
using System.Collections.Generic;
using Xunit;

namespace Studying.Test
{
    public class StudentProgressionShould
    {
        Guid StudentId { get; } = new Guid("b6308919-655b-421d-bac3-174ff1a8a3ae");
        CourseId CourseId { get; } = new CourseId(new Guid("0501bd8e-13fa-4669-a7c8-1b894963b712"));
        UnitId FirstUnit { get; } = new UnitId(new Guid("4370c240-b66b-48bc-bb51-63cdde8717c5"));
        List<UnitId> UnitIdList { get; } 
        
        public StudentProgressionShould()
        {
            UnitIdList = new List<UnitId>
            {
                FirstUnit,
                new UnitId(new Guid("2a2147ac-37d9-440d-9a2e-5b1f6c6f3d30")),
                new UnitId(new Guid("e27be7cd-dc2c-45ef-81e8-4af530023e0a"))
            };
        }
        
        [Fact]
        public void StartCourseWithFirstLesson()
        {
            var expected = FirstUnit;
            var courseLookupMock = new Mock<ICouserLookup>();
            courseLookupMock
                .Setup(cl => cl.GetUnits(CourseId))
                .Returns(UnitIdList);
            var root = new StudentProgressionAggregate(courseLookupMock.Object);
            var student = new Student(StudentId, "Pablo");
            var sut = root.CreateProgression(student);
            root.StartCourse(sut, CourseId);
            Assert.Equal(expected, sut.CurrentUnit);
        }

        [Fact]
        public void NotRestartCourse()
        {
            var expected = FirstUnit;
            var courseLookupMock = new Mock<ICouserLookup>();
            courseLookupMock
                .Setup(cl => cl.GetUnits(CourseId))
                .Returns(UnitIdList);
            var root = new StudentProgressionAggregate(courseLookupMock.Object);
            var student = new Student(StudentId, "Pablo");
            var sut = root.CreateProgression(student);
            root.StartCourse(sut, CourseId);
            Assert.Throws<ArgumentException>(() => root.StartCourse(sut, CourseId));
        }
    }
}
