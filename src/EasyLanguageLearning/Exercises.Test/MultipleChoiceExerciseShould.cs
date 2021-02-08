using EasyLanguageLearning.Domain.Shared.Kernel.Exercises;
using EasyLanguageLearning.Domain.Shared.Kernel.Units;
using Exercises.Domain;
using System;
using System.Collections.Generic;
using Xunit;

namespace Exercises.Test
{
    public class MultipleChoiceExerciseShould
    {
        
        [Fact]
        public void HaveChoices()
        {
            var root = new ExerciseAggregate();
            var content = UnitContentItem.Create("Si", "Yes");
            Assert.Throws<ArgumentException>(() => root.CreateMultipleChoiceExercise(0, new List<UnitContentItem>()));
        }
        //todo reverse translation testing
        [Theory]
        [InlineData(1, "Right", false)]
        [InlineData(2, "Wrong", false)]
        [InlineData(1, "Right", true)]
        [InlineData(2, "Wrong", true)]
        public void TelWhetherAnswerIsRightOrWrong(int answerIndex, string exptectedResultRaw, bool isLearningLanguage)
        {
            var expectedResult = Enum.Parse<TestResults>(exptectedResultRaw);
            var root = new ExerciseAggregate();
            var content = UnitContentItem.Create("Si", "yes");
            var choiceList = new List<UnitContentItem>
            {
                UnitContentItem.Create("Si", "yes"),
                UnitContentItem.Create("No", "No"),
                UnitContentItem.Create("Hola", "Hello"),
                UnitContentItem.Create("Adiós", "Good Bye")
            };
            var exercise = root.CreateMultipleChoiceExercise(0, choiceList, isLearningLanguage);
            var result = root.EvaluateMultipleChoiceExercise(exercise, answerIndex);

            Assert.Equal(expectedResult, result.Result);

        }
    }
    
}
