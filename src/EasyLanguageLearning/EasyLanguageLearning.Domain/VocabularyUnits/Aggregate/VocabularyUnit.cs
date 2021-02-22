using EasyLanguageLearning.Domain.Shared.Kernel.Languages;
using System;
using System.Collections.Generic;

namespace EasyLanguageLearning.Domain.VocabularyUnits.Aggregate
{
    public class VocabularyUnit
    {
        public VocabularyUnitId Id { get; protected set; }
        public ICollection<Vocabulary> VocabularyItems { get; protected set; } = new List<Vocabulary>();
        public Iso MotherLanguageIso { get; protected set; }
        public Iso LearningLanguageIso { get; protected set; }
        protected VocabularyUnit()
        {

        }
        public VocabularyUnit(Guid id, Iso motherLanguageIso, Iso learningLanguageIso)
        {
            Id = new VocabularyUnitId(id);
            MotherLanguageIso = motherLanguageIso;
            LearningLanguageIso = learningLanguageIso;
        }

        public void AddVocabulary(TranslatedContent term)
        {
            //Todo test that have matching isos
            VocabularyItems.Add(new Vocabulary(Guid.NewGuid(), Id, term));
        }
    }
}
