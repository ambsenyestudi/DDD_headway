using EasyLanguageLearning.Domain.Evaluations.Aggregate;
using EasyLanguageLearning.Domain.LearningPaths;
using EasyLanguageLearning.Domain.Shared.Kernel.Languages;
using EasyLanguageLearning.Domain.VocabularyUnits;
using EasyLanguageLearning.Domain.VocabularyUnits.Aggregate;
using System;
using System.Linq;
using Xunit;

namespace EasyLanguageLearning.Domain.Test.Evaluations
{
    public class WritingExerciseShould
    {
        public EvaluationBuilder builder;
        public readonly Iso EN_ISO = Iso.CreateIso(IsoCodes.en);
        public readonly Iso FR_ISO = Iso.CreateIso(IsoCodes.fr);
        private readonly VocabularyUnit vocabularyUnit;

        public WritingExerciseShould()
        {
            vocabularyUnit = new VocabularyUnit(Guid.NewGuid(),
                    new LessonId(Guid.NewGuid()),
                    EN_ISO,
                    FR_ISO);
            vocabularyUnit.AddVocabulary(TranslatedContent.Create(EN_ISO, "Yes", FR_ISO, "Oui"));
            builder = new EvaluationBuilder(Guid.NewGuid())
                .WithVocabularyUnits(vocabularyUnit);
                
        }

        [Fact]
        public void HaveContent()
        {
            var evaluation = builder.Build();
            Assert.Throws<ArgumentException>(() => evaluation.CreateWritingExercise(null));
        }
        
        [Theory]
        [InlineData("Oui", false)]
        [InlineData("Yes", true)]
        public void TellWhenAnswerIsRight(string answer, bool isLeaningLanguage)
        {
            var expectedResult = TestResults.Right;
            var evaluation = builder.Build();
            var vocabulary = vocabularyUnit.VocabularyItems.First();
            var exercise = evaluation.CreateWritingExercise(vocabulary, Guid.NewGuid(), isLeaningLanguage);
            var result = exercise.Evaluate(answer);

            Assert.Equal(expectedResult, result.Result);
        }
        
         [Fact]
         public void TellWhenAnswerIsAlmostRight()
         {
             var testAnswer = "Yes";
             var expectedResult = TestResults.Almost;

             var partialAnswer = testAnswer.Substring(0, 2);
             var evaluation = builder.Build();
             var vocabulary = vocabularyUnit.VocabularyItems.First();
             var exercise = evaluation.CreateWritingExercise(vocabulary, Guid.NewGuid(), true);
             var result = exercise.Evaluate(partialAnswer);

             Assert.Equal(expectedResult, result.Result);
         }

         [Fact]
         public void TellWhenAnswerIsWrong()
         {
             var expectedResult = TestResults.Wrong;

             var wrongAnswer = "1234567###";
            var evaluation = builder.Build();
            var vocabulary = vocabularyUnit.VocabularyItems.First();

            var exercise = evaluation.CreateWritingExercise(vocabulary, Guid.NewGuid());
             var result = exercise.Evaluate(wrongAnswer);

             Assert.Equal(expectedResult, result.Result);
         }

        [Theory]
        [InlineData("Go", "Goo", true)]
        [InlineData("Bo", "Bon", false)]
        public void AddACorrectLettreToTheRightPartOfYourAnswer(string currAnswer, string expectedTip, bool isLeaningLanguage)
        {

            vocabularyUnit.AddVocabulary(TranslatedContent.Create(EN_ISO, "Good morining", FR_ISO, "Bon jour"));
            var evaluation = builder.Build();
            var vocabulary = vocabularyUnit.VocabularyItems.Last();

            var exercise = evaluation.CreateWritingExercise(vocabulary, Guid.NewGuid(), isLeaningLanguage);
            var result = exercise.GetTip(currAnswer);

            Assert.Equal(expectedTip, result);
        }
         
    }
}
