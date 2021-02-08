using System;
using Xunit;
using Exercises.Domain;
using EasyLanguageLearning.Domain.Shared.Kernel.Units;
using EasyLanguageLearning.Domain.Shared.Kernel.Exercises;

namespace Exercises.Test
{
    public class WrittingExcersiseShould
    {
        [Fact]
        public void HaveContent()
        {
            var root = new ExerciseAggregate();
            Assert.Throws<ArgumentException>(()=>root.CreateWrittingExercise(UnitContentItem.Empty));
        }

        [Theory]
        [InlineData("Yes", false)]
        [InlineData("Si", true)]
        public void TellWhenAnswerIsRight(string answer, bool isLeaningLanguage)
        {
            var expectedResult = TestResults.Right;
            var root = new ExerciseAggregate();
            var content = UnitContentItem.Create("Si", "Yes");
            
            var exercise = root.CreateWrittingExercise(content, isLeaningLanguage);
            var result = root.EvaluateWrittingExercise(exercise, answer);
            
            Assert.Equal(expectedResult, result.Result);
        }

        [Fact]
        public void TellWhenAnswerIsAlmostRight()
        {
            var testAnswer = "Yes";
            var expectedResult = TestResults.Almost;
                        
            var partialAnswer = testAnswer.Substring(0, 2);
            var root = new ExerciseAggregate();
            var content = UnitContentItem.Create("Si", testAnswer);
            
            var exercise = root.CreateWrittingExercise(content);
            var result = root.EvaluateWrittingExercise(exercise, partialAnswer);

            Assert.Equal(expectedResult, result.Result);
        }

        [Fact]
        public void TellWhenAnswerIsWrong()
        {
            var expectedResult = TestResults.Wrong;

            var wrongAnswer = "1234567###";
            var root = new ExerciseAggregate();
            var content = UnitContentItem.Create("Si", "Yes");

            var exercise = root.CreateWrittingExercise(content);
            var result = root.EvaluateWrittingExercise(exercise, wrongAnswer);

            Assert.Equal(expectedResult, result.Result);
        }
        
        [Theory]
        [InlineData("Hel", "Hell")]
        [InlineData("Hol", "He")]
        public void AddACorrectLettreToTheRightPartOfYourAnswer(string currAnswer, string expectedTip)
        {
            var root = new ExerciseAggregate();
            var content = UnitContentItem.Create("Hola", "Hello");

            var exercise = root.CreateWrittingExercise(content);
            var result = root.GetTip(exercise, currAnswer);

            Assert.Equal(expectedTip, result);
        }
    }
}
