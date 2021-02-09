using Courses.Domain;
using EasyLanguageLearning.Domain.Shared.Kernel;
using Moq;
using Studying.Domain;
using System.Collections.Generic;

namespace Studying.Test
{
    public class CourseLookUpBuilder
    {
        private readonly Mock<ICourseLookup> courseLookupMock;
        private List<UnitId> unitList;
        private CourseId courseId;

        public CourseLookUpBuilder()
        {
            courseLookupMock = new Mock<ICourseLookup>();
        }
        public CourseLookUpBuilder WithCourseId(CourseId courseId)
        {
            this.courseId = courseId;
            return this;
        }
        public CourseLookUpBuilder WithUnits(List<UnitId> unitList)
        {
            this.unitList = unitList;
            return this;
        }

        public ICourseLookup Build()
        {
            courseLookupMock
                .Setup(cl => cl.GetUnits(courseId))
                .Returns(unitList);
            courseLookupMock
                .Setup(cl => cl.GetCourse(It.IsAny<LearningPathDefinition>()))
                .Returns(courseId);
            return courseLookupMock.Object;
        }
    }
}
