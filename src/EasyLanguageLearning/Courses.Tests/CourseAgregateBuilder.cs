using Courses.Domain;
using System.Linq;

namespace Courses.Tests
{
    public class CourseAgregateBuilder
    {
        private LanguageCatalog languageCatalog;
        private string motherLanguge;
        private string learningLanguage;

        public CourseAgregateBuilder WithLanguagesInCatalog(params IsoCodes[] isoCodeList)
        {
            var isoList = isoCodeList.Select(iso => Iso.CreateIso(iso));
            languageCatalog = new LanguageCatalog(isoList);
            return this;
        }
        public CourseAgregateBuilder WithMotherLanguage(string motherLanguge)
        {
            this.motherLanguge = motherLanguge;
            return this;
        }
        public CourseAgregateBuilder WithLearningLanguage(string learningLanguage)
        {
            this.learningLanguage = learningLanguage;
            return this;
        }
        public CourseAggregate Build()
        {
            return new CourseAggregate(languageCatalog);
        }
    }
}
