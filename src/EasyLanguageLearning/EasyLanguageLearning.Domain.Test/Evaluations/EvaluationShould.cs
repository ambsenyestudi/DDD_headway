using EasyLanguageLearning.Domain.Evaluations.Aggregate;
using EasyLanguageLearning.Domain.LearningPaths;
using EasyLanguageLearning.Domain.Shared.Kernel.Languages;
using EasyLanguageLearning.Domain.VocabularyUnits;
using EasyLanguageLearning.Domain.VocabularyUnits.Aggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EasyLanguageLearning.Domain.Test.Evaluations
{
    public class EvaluationShould
    {
        public readonly Iso EN_ISO = Iso.CreateIso(IsoCodes.en);
        public readonly Iso FR_ISO = Iso.CreateIso(IsoCodes.fr);
        private Guid evaluationId;
        private readonly VocabularyUnit vocabularyUnit;
        private readonly EvaluationBuilder builder;

        public EvaluationShould()
        {
            evaluationId = Guid.NewGuid();
            vocabularyUnit = new VocabularyUnit(Guid.NewGuid(),
                    new LessonId(Guid.NewGuid()),
                    EN_ISO,
                    FR_ISO);
            vocabularyUnit.AddVocabulary(TranslatedContent.Create(EN_ISO, "Yes", FR_ISO, "Oui"));
            builder = new EvaluationBuilder(Guid.NewGuid())
                .WithVocabularyUnits(vocabularyUnit);
        }

        [Fact]
        public void HaveAtLeastALessonWithVocabulary()
        {
            Assert.Throws<ArgumentException>(() => new Evaluation(evaluationId, null, null));
        }
        [Fact]
        public void HaveAllVocabularyBelongingToLessons()
        {
            var anotherUnit = new VocabularyUnit(Guid.NewGuid(),
                    new LessonId(Guid.NewGuid()),
                    EN_ISO,
                    FR_ISO);
            anotherUnit.AddVocabulary(TranslatedContent.Create(EN_ISO, "Great", FR_ISO, "Formidable"));
            

            Assert.Throws<ArgumentException>(() => builder.Build());
        }
    }
}
