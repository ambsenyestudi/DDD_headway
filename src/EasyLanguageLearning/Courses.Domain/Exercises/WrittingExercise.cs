using Courses.Domain.Translations;
using System.Linq;

namespace Courses.Domain.Exercises
{
    public class WrittingExercise
    {
        public string Heading { get; }
        private string TestAnswer { get; }
        public WrittingExercise(Translation translation, bool isReverse = false)
        {
            Heading = isReverse
                ? translation.TranslatedTerm
                : translation.OriginalTerm;
            TestAnswer = isReverse
                ? translation.OriginalTerm
                : translation.TranslatedTerm;
        }


        public ExerciseResult Evaluate(string writtenAnswer)
        {
            if (writtenAnswer == TestAnswer)
            {
                return ExerciseResult.CreateRightAnswer(TestAnswer);
            }

            var accuracyPercentage = FigureAccuracyPercentage(writtenAnswer, TestAnswer);
            if (accuracyPercentage < 0.5f)
            {
                return ExerciseResult.CreateWrongAnswer(TestAnswer);
            }
            return ExerciseResult.CreateAlmosCorrectAnswer(TestAnswer);
        }

        public string GetTip(string partialAnswer)
        {
            var tipIndex = 0;
            var count = 0;
            bool isDiscrepancy = false;
            while (!isDiscrepancy && count < TestAnswer.Length)
            {
                if (count < partialAnswer.Length)
                {
                    var nextAnswer = partialAnswer[count];
                    var nextCorrect = TestAnswer[count];
                    isDiscrepancy = nextAnswer != nextCorrect;
                    if (isDiscrepancy)
                    {
                        tipIndex = count;
                    }
                    count++;
                }
                else
                {
                    isDiscrepancy = true;
                    tipIndex = count;
                }
            }

            return TestAnswer.Substring(0, tipIndex + 1);

        }

        private float FigureAccuracyPercentage(string writtenAnswer, string translatedTerm)
        {
            float accuracyCount = translatedTerm.Intersect(writtenAnswer).Count();
            float totalCount = translatedTerm.Count();
            return accuracyCount / totalCount;
        }
    }
}
