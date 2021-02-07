using Courses.Domain;
using Courses.Tests.Extensions;
using System;
using System.Collections.Generic;
using Xunit;
using TC = Courses.Tests.AggregateTestConstants;

namespace Courses.Tests
{
    public class CourseShould
    {

        [Fact]
        public void LoadUnits()
        {
            var unitGuid = Guid.NewGuid();
            var definition = new CourseDefinition(TC.SPANISH_RAW_ISO, TC.ENGLISH_RAW_ISO, 1);
            var root = new CourseAgregateBuilder()
                .CreateSpanishEnglishCourse(new Dictionary<string, string>
                {
                    ["English"] = "Inglés"
                }, new Dictionary<Guid, string> 
                {
                    [unitGuid] = "Comienzo"
                });
            var sut = root.ChooseACourse(definition, unitGuid);
            Assert.NotEmpty(sut.UnitList);
        }
    }
}
