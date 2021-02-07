using EasyLanguageLearning.Domain.Shared.Kernel.Languages;
using System;
using System.Collections.Generic;
using Xunit;
using TC = Courses.Tests.AggregateTestConstants;
namespace Courses.Tests
{
    public class CourseAggregateShould
    {
        
        public CourseAggregateShould()
        {
            
        }
        [Fact]
        public void ChooseACourse()
        {
            var sut = new CourseAgregateBuilder()
                .WithLanguagesInCatalog(new Dictionary<IsoCodes, string> 
                { 
                    [TC.SPANISH_ISO_CODE]="Español",
                    [TC.ENGLISH_ISO_CODE] = "English"
                })
                .WithTranslations(new Dictionary<string, string>
                {
                    ["English"] = "Inglés"
                })
                .Build();
            var result = sut.ChooseACourse(TC.SPANISH_RAW_ISO, TC.ENGLISH_RAW_ISO, 1, Guid.Empty);
            Assert.NotNull(result);
        }
        [Fact]
        public void GetNotGetCoursesWithSameLanguage()
        {
            var sameLang = TC.SPANISH_RAW_ISO;
            var sut = new CourseAgregateBuilder()
                .WithLanguagesInCatalog(new Dictionary<IsoCodes, string>
                {
                    [TC.SPANISH_ISO_CODE] = "Español",
                })
                .Build();
            Assert.Throws<ArgumentException>(()=> sut.ChooseACourse(sameLang, sameLang, 1, Guid.Empty));
        }
        [Theory]
        [InlineData("x81","es")]
        [InlineData("es", "x81")]
        public void NotGetCourseWhenInvalidIsoCodes(string motherLanguage, string learningLanguage)
        {
            var sut = new CourseAgregateBuilder()
               .WithLanguagesInCatalog(new Dictionary<IsoCodes, string>
               {
                   [TC.SPANISH_ISO_CODE] = "Español",
                   [TC.ENGLISH_ISO_CODE] = "English"
               })
               .Build();
            Assert.Throws<ArgumentException>(() => sut.ChooseACourse(motherLanguage, learningLanguage, 1, Guid.Empty));
        }

        [Fact]
        public void GetCourseNameInMotherLangauge()
        {
            var exptected = "Inglés";
            var sut = new CourseAgregateBuilder()
               .WithLanguagesInCatalog(new Dictionary<IsoCodes, string>
               {
                   [TC.SPANISH_ISO_CODE] = "Español",
                   [TC.ENGLISH_ISO_CODE] = "English"
               })
               .WithTranslations(new Dictionary<string, string>
               {
                   ["English"] = "Inglés"
               })
               .Build();
            var result = sut.ChooseACourse(TC.SPANISH_RAW_ISO, TC.ENGLISH_RAW_ISO, 1, Guid.Empty);
            Assert.Contains(exptected, result.Name);
        }
        
    }
}
