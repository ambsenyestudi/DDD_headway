using System;
using System.Collections.Generic;

namespace EasyLanguageLearning.Domain.Shared.Kernel.Exercises
{
    public class ExerciseResult: ValueObject
    {
        public static ExerciseResult Empty { get; } = new ExerciseResult(string.Empty, TestResults.None);
        public TestResults Result { get; }
        public string CorrectAnswer { get; }
        
        protected ExerciseResult(string correctAnswer, TestResults result)
        {
            this.CorrectAnswer = correctAnswer;
            this.Result = result;
        }

        protected override IEnumerable<object> GetEqualityComponents() =>
            new object[]
            {
                Result,
                CorrectAnswer
            };

        public static ExerciseResult Create(string correctAnswer, TestResults result)
        {
            if(string.IsNullOrEmpty(correctAnswer) || result == TestResults.None)
            {
                return Empty;
            }
            return new ExerciseResult(correctAnswer, result);
        }
    }
}
