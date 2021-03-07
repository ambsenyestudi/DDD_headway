using System;

namespace EasyLanguageLearning.Application.VocabularyUnits
{
    public class VocabularyUnitDTO
    {
        public Guid Id { get; set; }
        public Guid LessonId { get; set; }
        public string LearningIso { get; set; }
        public string MotherIso { get; set; }

        public TranslationDTO[] VocabularyList { get; set; }
    }
}
