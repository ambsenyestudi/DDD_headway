using Courses.Domain.Exceptions;
using Courses.Domain.Translations;
using EasyLanguageLearning.Domain.Shared.Kernel;
using EasyLanguageLearning.Domain.Shared.Kernel.Languages;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using TC = Courses.Tests.AggregateTestConstants;

namespace Courses.Tests
{
    public class UnitShould
    {
        public readonly Guid UNIT_ID = new Guid("eec6b9bc-441a-45f0-8ea7-7ca2e244991c");
        public const string UNIT_NAME = "Comienzo";
        public readonly Iso SPANISH_ISO = Iso.CreateIso(TC.SPANISH_ISO_CODE);
        public readonly Iso ENGLISH_ISO = Iso.CreateIso(TC.ENGLISH_ISO_CODE);
        public readonly Iso FRENCH_ISO = Iso.CreateIso(IsoCodes.fr);

        [Fact]
        public void NotHaveRepeatedContent()
        {
            var root = new CourseAgregateBuilder()
                .WithLanguagesInCatalog(new Dictionary<IsoCodes, string>
                {
                    [TC.SPANISH_ISO_CODE] = "Español",
                    [TC.ENGLISH_ISO_CODE] = "English"
                })
                .WithTranslations(new Dictionary<string, string>
                {
                    ["English"] = "Inglés"
                })
                .WithUnits(new Dictionary<Guid, string> { [UNIT_ID]= UNIT_NAME })
                .Build();
            var course = root.ChooseACourse(TC.SPANISH_RAW_ISO, TC.ENGLISH_RAW_ISO, 1, Guid.Empty);
            var repeatdTranslationList = new List<Translation>
            { 
                Translation.Create(SPANISH_ISO, ENGLISH_ISO, "Si","Yes"),
                Translation.Create(SPANISH_ISO, ENGLISH_ISO, "Si","Yes"),
            };
            Assert.Throws<RepeatedUnitContentException>(() => root.LoadUnitContent(course, UNIT_ID, repeatdTranslationList));
        }
        [Fact]
        public void LoadAllUnitContent()
        {
            var translationList = new List<Translation>
            {
                Translation.Create(SPANISH_ISO, ENGLISH_ISO, "Si","Yes"),
                Translation.Create(SPANISH_ISO, ENGLISH_ISO, "No","No"),
            };
            var exptectedTranslationCount = translationList.Count;
            var root = new CourseAgregateBuilder()
                .WithLanguagesInCatalog(new Dictionary<IsoCodes, string>
                {
                    [TC.SPANISH_ISO_CODE] = "Español",
                    [TC.ENGLISH_ISO_CODE] = "English"
                })
                .WithTranslations(new Dictionary<string, string>
                {
                    ["English"] = "Inglés"
                })
                .WithUnits(new Dictionary<Guid, string> { [UNIT_ID] = UNIT_NAME })
                .Build();
            var course = root.ChooseACourse(TC.SPANISH_RAW_ISO, TC.ENGLISH_RAW_ISO, 1, Guid.Empty);
            root.LoadUnitContent(course, UNIT_ID, translationList);
            var actualCount = course.UnitList.First().Content.Count;
            Assert.Equal(exptectedTranslationCount, actualCount);
        }

        [Fact]
        public void HaveAllContentInLanguage()
        {
            var translationList = new List<Translation>
            {
                Translation.Create(SPANISH_ISO, FRENCH_ISO, "Si","Oui"),
                Translation.Create(FRENCH_ISO, ENGLISH_ISO, "Non","No"),
            };
            var root = new CourseAgregateBuilder()
                .WithLanguagesInCatalog(new Dictionary<IsoCodes, string>
                {
                    [TC.SPANISH_ISO_CODE] = "Español",
                    [TC.ENGLISH_ISO_CODE] = "English",
                    [IsoCodes.fr] = "Francais"
                })
                .WithTranslations(new Dictionary<string, string>
                {
                    ["English"] = "Inglés"
                })
                .WithUnits(new Dictionary<Guid, string> { [UNIT_ID] = UNIT_NAME })
                .Build();
            var course = root.ChooseACourse(TC.SPANISH_RAW_ISO, TC.ENGLISH_RAW_ISO, 1, Guid.Empty);

            Assert.Throws<InvalidUnitContentException>(() => root.LoadUnitContent(course, UNIT_ID, translationList));
        }
    }
}
