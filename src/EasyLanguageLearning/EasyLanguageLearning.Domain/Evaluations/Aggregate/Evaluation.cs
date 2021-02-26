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
            Id = new EvaluationId(id);
            LessonCollection = lessons;
            VocabularyIdCollection = vocabularyItems.Select(vo => vo.Id).ToList();
        }

        
    }
}
