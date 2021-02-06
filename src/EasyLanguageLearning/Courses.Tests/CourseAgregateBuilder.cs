using Courses.Domain;
using EasyLanguageLearning.Domain.Shared.Kernel.Languages;
using System.Collections.Generic;
using System.Linq;

namespace Courses.Tests
{
    public class CourseAgregateBuilder
    {
        private LanguageCatalog languageCatalog;

        public CourseAgregateBuilder WithLanguagesInCatalog(Dictionary<IsoCodes,string> languagesDictionary)
        {
            var languageList = languagesDictionary
                .Select((it) => Language.CreateFromNameAndIso(it.Value, Iso.CreateIso(it.Key)))
                .ToList();
            languageCatalog = new LanguageCatalog(languageList);
            return this;
        }
        public CourseAggregate Build()
        {
            return new CourseAggregate(languageCatalog);
        }
    }
}
