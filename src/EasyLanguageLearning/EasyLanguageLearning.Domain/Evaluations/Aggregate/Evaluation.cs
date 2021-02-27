using EasyLanguageLearning.Domain.LearningPaths;
using EasyLanguageLearning.Domain.VocabularyUnits;
using EasyLanguageLearning.Domain.VocabularyUnits.Aggregate;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EasyLanguageLearning.Domain.Evaluations.Aggregate
{
    public class Evaluation
    {
        public EvaluationId Id { get; protected set; }
        public ICollection<LessonId> LessonCollection { get; protected set; }
        public ICollection<VocabularyId> VocabularyIdCollection { get; protected set; }

        public Evaluation(Guid id, List<LessonId> lessons, List<Vocabulary> vocabularyItems)
        {
            //check all voc items are in lessons
            Id = new EvaluationId(id);
            LessonCollection = lessons;
            VocabularyIdCollection = vocabularyItems.Select(vo => vo.Id).ToList();
        }

        public WritingExercise CreateWritingExercise(Vocabulary vocabulary, Guid id = new Guid(), bool isLearningLanguageHeading = false)
        {
            //Todo check if writing exercises belong to evalutaion
            if (vocabulary == null)
            {
                throw new ArgumentException($"Can't {nameof(CreateWritingExercise)}");
            }
            if (id == Guid.Empty)
            {
                id = Guid.NewGuid();
            }
            return new WritingExercise(id, vocabulary, isLearningLanguageHeading);
        }
    }
}
