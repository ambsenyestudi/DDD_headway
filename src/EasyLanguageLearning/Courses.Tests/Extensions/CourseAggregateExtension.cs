using Courses.Domain;
using EasyLanguageLearning.Domain.Shared.Kernel.Languages;
using System;
using System.Collections.Generic;

using TC = Courses.Tests.AggregateTestConstants;

namespace Courses.Tests.Extensions
{
    public static class CourseAggregateExtension
    {
        public static CourseAggregate CreateSpanishEnglishCourse(this CourseAgregateBuilder builder,
            Dictionary<string, string> translations) =>
            builder.AddSpanishEnglishCatalog()
            .WithTranslations(translations)
            .Build();
        public static CourseAggregate CreateSpanishEnglishCourse(this CourseAgregateBuilder builder,
            Dictionary<string, string> translations, Dictionary<Guid, string> unitsIdNameDictionary) =>
            builder.AddSpanishEnglishCatalog()
            .WithTranslations(translations)
            .WithUnits(unitsIdNameDictionary)
            .Build();


        public static CourseAggregate CreateSpanishEnglishCourse(this CourseAgregateBuilder builder) =>
            builder.AddSpanishEnglishCatalog()
               .Build();

        private static CourseAgregateBuilder AddSpanishEnglishCatalog(this CourseAgregateBuilder builder) => 
            builder.WithLanguagesInCatalog(new Dictionary<IsoCodes, string>
            {
                [TC.SPANISH_ISO_CODE] = "Español",
                [TC.ENGLISH_ISO_CODE] = "English"
            });

    }
}
