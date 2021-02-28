using EasyLanguageLearning.Domain.Evaluations.Aggregate;
using EasyLanguageLearning.Domain.VocabularyUnits;
using EasyLanguageLearning.Domain.VocabularyUnits.Aggregate;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EasyLanguageLearning.Domain.Test.Evaluations
{
    public class EvaluationBuilder
    {
        private Guid id;
        public VocabularyUnit[] unitList;

        public EvaluationBuilder (Guid id) {
            this.id = id;
        }
        public EvaluationBuilder WithVocabularyUnits(params VocabularyUnit[] units)
        {
            this.unitList = units;
            return this;
        }

        public Evaluation Build()
        {
            var lessonIdList = unitList.Select(u => u.LessonId).ToList();
            var items = unitList.SelectMany(u => u.VocabularyItems).ToList();//.Union(vocabularyItems).ToList();
            return new Evaluation(id, lessonIdList, items);
        }
    }
}
