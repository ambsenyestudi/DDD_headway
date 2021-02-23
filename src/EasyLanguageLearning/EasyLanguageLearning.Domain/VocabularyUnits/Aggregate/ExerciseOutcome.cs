using EasyLanguageLearning.Domain.Shared.Kernel;
using System;
using System.Collections.Generic;

namespace EasyLanguageLearning.Domain.VocabularyUnits.Aggregate
{
    public enum TestResults
    {
        None, Wrong, Almost, Right
    }
    public class ExerciseOutcome:ValueObject
    {
        public static ExerciseOutcome Empty { get; } = new ExerciseOutcome(string.Empty, TestResults.None);
        public TestResults Result { get; }
        public string CorrectAnswer { get; }

        protected ExerciseOutcome(string correctAnswer, TestResults result)
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

        public static ExerciseOutcome Create(string correctAnswer, TestResults result)
        {
            if (string.IsNullOrEmpty(correctAnswer) || result == TestResults.None)
            {
                return Empty;
            }
            return new ExerciseOutcome(correctAnswer, result);
        }

        internal static ExerciseOutcome CreateWrongAnswer(string testAnswer) =>
            Create(testAnswer, TestResults.Wrong);

        internal static ExerciseOutcome CreateAlmosCorrectAnswer(string testAnswer) => 
            Create(testAnswer, TestResults.Almost);

        internal static ExerciseOutcome CreateRightAnswer(string answer) =>
            Create(answer, TestResults.Right);

    }
}
