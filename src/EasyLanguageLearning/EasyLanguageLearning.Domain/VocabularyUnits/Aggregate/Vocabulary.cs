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
        public VocabularyUnit VocabularyUnit { get; protected set;  }
        protected Vocabulary()
        {
        }
        #endregion Ef core

        public Vocabulary(Guid id, VocabularyUnit unit, TranslatedContent translatedContent)
        {
            Id = new VocabularyId(id);
            VocabularyUnitId = unit.Id;
            VocabularyUnit = unit;
            MotherLanguageTerm = translatedContent.MotherLanguageTerm;
            LearningLanguageTerm = translatedContent.LearningLanguageTerm;

        }


        
    }
}
