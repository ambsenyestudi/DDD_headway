using Courses.Domain;
using EasyLanguageLearning.Domain.Shared.Kernel.Languages;
using System;
using System.Collections.Generic;
using Xunit;

namespace Courses.Tests
{
    public class CourseAggregateShould
    {
        private const IsoCodes SPANISH_ISO_CODE = IsoCodes.es;
        private const IsoCodes ENGLISH_ISO_CODE = IsoCodes.en;
        private readonly string SPANISH_RAW_ISO = SPANISH_ISO_CODE.ToString();
        private readonly string ENGLISH_RAW_ISO = ENGLISH_ISO_CODE.ToString();
        private readonly Language SPANISH_LANG = Language.CreateFromNameAndIso("Español", Iso.CreateIso(SPANISH_ISO_CODE));
        private readonly Language ENGLISH_LANG = Language.CreateFromNameAndIso("Español", Iso.CreateIso(ENGLISH_ISO_CODE));
        public CourseAggregateShould()
        {
            
        }
        [Fact]
        public void ChooseACourse()
        {
            var sut = new CourseAgregateBuilder()
                .WithLanguagesInCatalog(new Dictionary<IsoCodes, string> 
                { 
                    [SPANISH_ISO_CODE]="Español",
                    [ENGLISH_ISO_CODE] = "English"
                })
                .Build();
            var result = sut.ChooseACourse(SPANISH_RAW_ISO, ENGLISH_RAW_ISO);
            Assert.NotNull(result);
        }
        [Fact]
        public void GetNotGetCoursesWithSameLanguage()
        {
            var sameLang = SPANISH_RAW_ISO;
            var sut = new CourseAgregateBuilder()
                .WithLanguagesInCatalog(new Dictionary<IsoCodes, string>
                {
                    [SPANISH_ISO_CODE] = "Español",
                })
                .Build();
            Assert.Throws<ArgumentException>(()=> sut.ChooseACourse(sameLang, sameLang));
        }
        [Theory]
        [InlineData("x81","es")]
        [InlineData("es", "x81")]
        public void NotGetCourseWhenInvalidIsoCodes(string motherLanguage, string learningLanguage)
        {
            var sut = new CourseAgregateBuilder()
               .WithLanguagesInCatalog(new Dictionary<IsoCodes, string>
               {
                   [SPANISH_ISO_CODE] = "Español",
                   [ENGLISH_ISO_CODE] = "English"
               })
               .Build();
            Assert.Throws<ArgumentException>(() => sut.ChooseACourse(motherLanguage, learningLanguage));
        }

        [Fact]
        public void GetCourseNameInMotherLangauge()
        {
            var exptected = "Inglés";
            var sut = new CourseAgregateBuilder()
               .WithLanguagesInCatalog(new Dictionary<IsoCodes, string>
               {
                   [SPANISH_ISO_CODE] = "Español",
                   [ENGLISH_ISO_CODE] = "English"
               })
               .Build();
            var result = sut.ChooseACourse(SPANISH_RAW_ISO, ENGLISH_RAW_ISO);
            Assert.Contains(exptected, result.Name);
        }
    }
}
