using EasyLanguageLearning.Domain.Shared.Kernel.Exercises;

namespace Exercises.Domain
{
    public class ExerciseOutcome : ExerciseResult
    {
        protected ExerciseOutcome(string correctAnswer, TestResults result) : base(correctAnswer, result)
        {
        }
        
        public static ExerciseOutcome CreateRightAnswer(string correctAnswer) =>
            new ExerciseOutcome(correctAnswer, TestResults.Right);

        public static ExerciseOutcome CreateWrongAnswer(string correctAnswer) =>
            new ExerciseOutcome(correctAnswer, TestResults.Wrong);
        public static ExerciseOutcome CreateAlmosCorrectAnswer(string correctAnswer) =>
           new ExerciseOutcome(correctAnswer, TestResults.Almost);
        
    }
}
