using EasyLanguageLearning.Domain.Shared.Kernel.Languages;
using System;

namespace EasyLanguageLearning.Domain.VocabularyUnits.Aggregate
{
    public class Vocabulary
    {
        public VocabularyId Id { get; protected set; }
        public VocabularyUnitId VocabularyUnitId { get; protected set; }

        #region EF core
        public string MotherLanguageTerm { get; protected set; }
        public string LearningLanguageTerm { get; protected set; }
        protected Vocabulary()
        {
        }
        #endregion Ef core

        public Vocabulary(Guid id, VocabularyUnitId unitId, TranslatedContent translatedContent)
        {
            Id = new VocabularyId(id);
            VocabularyUnitId = unitId;
            MotherLanguageTerm = translatedContent.MotherLanguageTerm;
            LearningLanguageTerm = translatedContent.LearningLanguageTerm;

        }


        
    }
}
