using EasyLanguageLearning.Domain.Evaluations.Aggregate;
using EasyLanguageLearning.Domain.LearningPaths;
using EasyLanguageLearning.Domain.Shared.Kernel.Languages;
using EasyLanguageLearning.Domain.VocabularyUnits;
using EasyLanguageLearning.Domain.VocabularyUnits.Aggregate;
using System;
using System.Linq;
using Xunit;

namespace EasyLanguageLearning.Domain.Test.VocabularyUnits
{
    public class WritingExerciseShould
    {
        public readonly Iso EN_ISO = Iso.CreateIso(IsoCodes.en);
        public readonly Iso FR_ISO = Iso.CreateIso(IsoCodes.fr);
        private readonly VocabularyUnit vocabularyUnit;
        /*
        public WritingExerciseShould()
        {
            var id = Guid.NewGuid();
            var lessonId = new LessonId(Guid.NewGuid());
            vocabularyUnit = new VocabularyUnit(id, lessonId, EN_ISO, FR_ISO);
        }

        [Fact]
        public void HaveContent()
        {
            Assert.Throws<ArgumentException>(() => vocabularyUnit.CreateWritingExercise(null));
        }

        [Theory]
        [InlineData("Oui", false)]
        [InlineData("Yes", true)]
        public void TellWhenAnswerIsRight(string answer, bool isLeaningLanguage)
        {
            var expectedResult = TestResults.Right;
            var content = TranslatedContent.Create(EN_ISO, "Yes", FR_ISO, "Oui");
            vocabularyUnit.AddVocabulary(content);
            var vocabulary = vocabularyUnit.VocabularyItems.First();
            var exercise = vocabularyUnit.CreateWritingExercise(vocabulary, Guid.NewGuid(), isLeaningLanguage);
            var result = exercise.Evaluate(answer);

            Assert.Equal(expectedResult, result.Result);
        }

        [Fact]
        public void TellWhenAnswerIsAlmostRight()
        {
            var testAnswer = "Yes";
            var expectedResult = TestResults.Almost;

            var partialAnswer = testAnswer.Substring(0, 2);
            var content = TranslatedContent.Create(EN_ISO, "Yes", FR_ISO, "Oui");
            vocabularyUnit.AddVocabulary(content);
            var vocabulary = vocabularyUnit.VocabularyItems.First();
            var exercise = vocabularyUnit.CreateWritingExercise(vocabulary, Guid.NewGuid(), true);
            var result = exercise.Evaluate(partialAnswer);

            Assert.Equal(expectedResult, result.Result);
        }

        [Fact]
        public void TellWhenAnswerIsWrong()
        {
            var expectedResult = TestResults.Wrong;

            var wrongAnswer = "1234567###";
            var content = TranslatedContent.Create(EN_ISO, "Yes", FR_ISO, "Oui");
            vocabularyUnit.AddVocabulary(content);
            var vocabulary = vocabularyUnit.VocabularyItems.First();

            var exercise = vocabularyUnit.CreateWritingExercise(vocabulary, Guid.NewGuid());
            var result = exercise.Evaluate(wrongAnswer);

            Assert.Equal(expectedResult, result.Result);
        }

        [Theory]
        [InlineData("Go", "Goo", true)]
        [InlineData("Bo", "Bon", false)]
        public void AddACorrectLettreToTheRightPartOfYourAnswer(string currAnswer, string expectedTip, bool isLeaningLanguage)
        {
            var content = TranslatedContent.Create(EN_ISO, "Good morining", FR_ISO, "Bon jour");
            vocabularyUnit.AddVocabulary(content);
            var vocabulary = vocabularyUnit.VocabularyItems.First();

            var exercise = vocabularyUnit.CreateWritingExercise(vocabulary, Guid.NewGuid(), isLeaningLanguage);
            var result = exercise.GetTip(currAnswer);

            Assert.Equal(expectedTip, result);
        }
        */
    }
}
