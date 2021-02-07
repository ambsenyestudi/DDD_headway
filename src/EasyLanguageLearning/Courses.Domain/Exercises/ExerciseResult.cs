namespace Courses.Domain.Exercises
{
    public class ExerciseResult
    {
        public string CorrectAnswer { get; }
        public TestResults Results { get; }
        protected ExerciseResult(string correctAnswer, TestResults result)
        {
            this.CorrectAnswer = correctAnswer;
            this.Results = result;
        }
        public static ExerciseResult CreateRightAnswer(string correctAnswer)=>
            new ExerciseResult(correctAnswer, TestResults.Right);

        public static ExerciseResult CreateWrongAnswer(string correctAnswer) =>
            new ExerciseResult(correctAnswer, TestResults.Wrong);
        public static ExerciseResult CreateAlmosCorrectAnswer(string correctAnswer) =>
           new ExerciseResult(correctAnswer, TestResults.Almost);
    }
}
