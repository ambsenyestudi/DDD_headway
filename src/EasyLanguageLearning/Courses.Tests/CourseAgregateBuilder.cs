using Courses.Domain;
using Courses.Domain.Languages;
using Courses.Domain.Translations;
using EasyLanguageLearning.Domain.Shared.Kernel.Languages;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Courses.Tests
{
    public class CourseAgregateBuilder
    {
        private List<Language> languageList;
        private Dictionary<string, string> translationDictionary = new Dictionary<string, string>();
        private List<Unit> unitList = new List<Unit>();

        private readonly Mock<IUnitLookUp> unitLookUpMock = new Mock<IUnitLookUp>();
        private readonly Mock<ILanguageLookUp> languageLookupMock = new Mock<ILanguageLookUp>();
        private readonly Mock<ITranslationLookUp> translationLookupMock = new Mock<ITranslationLookUp>();
        public CourseAgregateBuilder()
        {

        }
        public CourseAgregateBuilder WithLanguagesInCatalog(Dictionary<IsoCodes,string> languagesDictionary)
        {
            languageList = languagesDictionary
                .Select((it) => Language.CreateFromNameAndIso(it.Value, Iso.CreateIso(it.Key)))
                .ToList();
            return this;
        }
        public CourseAgregateBuilder WithTranslations(Dictionary<string, string> translationDictionary)
        {
            this.translationDictionary = translationDictionary;
            return this;
        }
        public CourseAgregateBuilder WithUnits(Dictionary<Guid,string> unitIdNameDictionary)
        {
            unitList = unitIdNameDictionary
                .Select((kv) => new Unit(kv.Key, kv.Value))
                .ToList();
            return this;
        }
        public CourseAggregate Build()
        {
            
            languageLookupMock
                .Setup(x => x.GetLanguage(It.IsAny<Iso>()))
                .Returns((Iso iso) => CreateLangauge(iso, languageList));
            languageLookupMock
                .Setup(x => x.CatalogContains(It.IsAny<Iso>()))
                .Returns((Iso iso) => languageList.Any(l => l.Iso == iso));
            
            translationLookupMock
                .Setup(t => t.Translate(It.IsAny<Iso>(), It.IsAny<Iso>(), It.IsAny<string>()))
                .Returns((Iso from, Iso to, string term) => translationDictionary[term]);

            unitLookUpMock.Setup(ul => ul.GetUnits(It.IsAny<Guid>()))
                .Returns(unitList);
            return new CourseAggregate(languageLookupMock.Object, translationLookupMock.Object, unitLookUpMock.Object);
        }

        private Language CreateLangauge(Iso iso, List<Language> languageList)
        {
            var name = languageList.First(l => l.Iso == iso).Name;
            return Language.CreateFromNameAndIso(name, iso);
        }
    }
}
