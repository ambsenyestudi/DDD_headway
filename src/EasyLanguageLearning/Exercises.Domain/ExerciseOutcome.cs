using EasyLanguageLearning.Domain.Shared.Kernel.Exercises;

namespace Exercises.Domain
{
    public class ExerciseOutcome : ExerciseResult
    {
        protected ExerciseOutcome(string correctAnswer, TestResults result) : base(correctAnswer, result)
        {
        }
        /*
        public static ExerciseResult CreateRightAnswer(string correctAnswer) =>
            new ExerciseResult(correctAnswer, TestResults.Right);

        public static ExerciseResult CreateWrongAnswer(string correctAnswer) =>
            new ExerciseResult(correctAnswer, TestResults.Wrong);
        public static ExerciseResult CreateAlmosCorrectAnswer(string correctAnswer) =>
           new ExerciseResult(correctAnswer, TestResults.Almost);
        */
    }
}
