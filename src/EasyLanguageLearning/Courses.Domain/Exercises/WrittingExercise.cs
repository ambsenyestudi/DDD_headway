using Courses.Domain.Translations;
using System.Linq;

namespace Courses.Domain.Exercises
{
    public class WrittingExercise
    {
        public string Heading { get; }

        private string Answer { get; }
        public WrittingExercise(Translation translation, bool isReverse = false)
        {
            Heading = isReverse
                ? translation.TranslatedTerm
                : translation.OriginalTerm;
            Answer = isReverse
                ? translation.OriginalTerm
                : translation.TranslatedTerm;
        }


        public ExerciseResult Evaluate(string writtenAnswer)
        {
            if (writtenAnswer == Answer)
            {
                return ExerciseResult.CreateRightAnswer(Answer);
            }

            var accuracyPercentage = FigureAccuracyPercentage(writtenAnswer, Answer);
            if (accuracyPercentage < 0.5f)
            {
                return ExerciseResult.CreateWrongAnswer(Answer);
            }
            return ExerciseResult.CreateAlmosCorrectAnswer(Answer);
        }

        public string GetTip(string partialAnswer)
        {
            var tipIndex = 0;
            var count = 0;
            bool isDiscrepancy = false;
            while (!isDiscrepancy && count < Answer.Length)
            {
                if (count < partialAnswer.Length)
                {
                    var nextAnswer = partialAnswer[count];
                    var nextCorrect = Answer[count];
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

            return Answer[tipIndex].ToString();

        }

        private float FigureAccuracyPercentage(string writtenAnswer, string translatedTerm)
        {
            float accuracyCount = translatedTerm.Intersect(writtenAnswer).Count();
            float totalCount = translatedTerm.Count();
            return accuracyCount / totalCount;
        }
    }
}
