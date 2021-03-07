using System;

namespace EasyLanguageLearning.Application.LanguageCatalog
{
    public class LearningLanguageDTO
    {
        public Guid Id { get; set; }
        public Guid LanguageCatalogId { get; set; }
        public string Name { get; set; }
        public string Iso { get; set; }
        public string MotherIso { get; set; }
    }
}
