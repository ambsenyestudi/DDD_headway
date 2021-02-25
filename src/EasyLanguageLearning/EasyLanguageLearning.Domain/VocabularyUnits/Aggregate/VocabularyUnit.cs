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
        public ICollection<Vocabulary> VocabularyItems { get; protected set; } = new List<Vocabulary>();
        public Iso MotherLanguageIso { get; protected set; }
        public Iso LearningLanguageIso { get; protected set; }
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
            VocabularyItems.Add(new Vocabulary(Guid.NewGuid(), Id, term));
        }

        public WritingExercise CreateWritingExercise(Vocabulary vocabulary, Guid id = new Guid(), bool isLearningLanguageHeading = false)
        {
            if(vocabulary == null)
            {
                throw new ArgumentException($"Can't {nameof(CreateWritingExercise)}");
            }
            if(id ==  Guid.Empty)
            {
                id = Guid.NewGuid();
            }
            return new WritingExercise(id, vocabulary, isLearningLanguageHeading);
        }
    }
}
