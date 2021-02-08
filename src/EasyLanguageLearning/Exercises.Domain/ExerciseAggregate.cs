using EasyLanguageLearning.Domain.Shared.Kernel.Exercises;
using EasyLanguageLearning.Domain.Shared.Kernel.Units;
using System;
using System.Collections.Generic;

namespace Exercises.Domain
{
    public class ExerciseAggregate
    {
        public WrittingExercise CreateWrittingExercise(UnitContentItem content)
        {
            return new WrittingExercise();
        }

        public ExerciseResult EvaluateExercise(WrittingExercise exercise, ExerciseSolution studentSolution)
        {
            return ExerciseResult.Empty;
        }
    }
}
