using EasyLanguageLearning.Domain.LearningPaths;
using EasyLanguageLearning.Domain.Shared.Kernel.Languages;
using System;
using System.Collections.Generic;

namespace EasyLanguageLearning.Domain.VocabularyUnits.Aggregate
{
    public class VocabularyUnit
    {
        public VocabularyUnitId Id { get; protected set; }
        public LessonId LessonId { get; protected set; }
        private VocabularyCollection vocabularyItems = new VocabularyCollection();
        public Iso MotherLanguageIso { get; protected set; }
        public Iso LearningLanguageIso { get; protected set; }
        public bool HasIems { get => !vocabularyItems.IsEmpty; }

        protected VocabularyUnit()
        {

        }
        public VocabularyUnit(Guid id, LessonId lessonId, Iso motherLanguageIso, Iso learningLanguageIso)
        {
            Id = new VocabularyUnitId(id);
            LessonId = lessonId;
            MotherLanguageIso = motherLanguageIso;
            LearningLanguageIso = learningLanguageIso;
        }

        public void AddVocabulary(TranslatedContent term)
        {
            //Todo test that have matching isos
            vocabularyItems.Add(new Vocabulary(Guid.NewGuid(), this, term));
        }
        public IEnumerable<Vocabulary> ListItems() =>
            vocabularyItems.ListVocabulary();
        
    }
}
