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
        public void TellWhenAnswerIsRight()
        {
            var exptectedOutcome = TestResults.Right;
            var root = new ExerciseAggregate();
            var exercise = root.CreateWrittingExercise(null);
            var content = UnitContentItem.Empty;
            var answer = new ExerciseSolution(content);
            var result = root.EvaluateExercise(exercise, answer);
            Assert.Equal(exptectedOutcome, result.Result);
        }
    }
}
