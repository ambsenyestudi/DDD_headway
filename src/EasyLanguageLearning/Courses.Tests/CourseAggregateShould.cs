using Courses.Domain;
using System;
using Xunit;

namespace Courses.Tests
{
    public class CourseAggregateShould
    {
        private const IsoCodes SPANISH_ISO_CODE = IsoCodes.es;
        private const IsoCodes ENGLISH_ISO_CODE = IsoCodes.en;
        private readonly string SPANISH_LANG = SPANISH_ISO_CODE.ToString();
        private readonly string ENGLISH_LANG = ENGLISH_ISO_CODE.ToString();
        public CourseAggregateShould()
        {
            
        }
        [Fact]
        public void ChooseACourse()
        {
            var sut = new CourseAgregateBuilder()
                .WithLanguagesInCatalog(SPANISH_ISO_CODE, ENGLISH_ISO_CODE)
                .Build();
            var result = sut.ChooseACourse(SPANISH_LANG, ENGLISH_LANG);
            Assert.NotNull(result);
        }
        [Fact]
        public void GetNotGetCoursesWithSameLanguage()
        {
            var sameLang = ENGLISH_LANG;
            var sut = new CourseAgregateBuilder()
                .WithLanguagesInCatalog(SPANISH_ISO_CODE, SPANISH_ISO_CODE)
                .Build();
            Assert.Throws<ArgumentException>(()=> sut.ChooseACourse(sameLang, sameLang));
        }
        [Theory]
        [InlineData("x81","es")]
        [InlineData("es", "x81")]
        public void HaveValidIsoCodes(string motherLanguage, string learningLanguage)
        {
            var sut = new CourseAgregateBuilder()
               .WithLanguagesInCatalog(SPANISH_ISO_CODE, SPANISH_ISO_CODE)
               .Build();
            Assert.Throws<ArgumentException>(() => sut.ChooseACourse(motherLanguage, learningLanguage));
        }
    }
}
