using EasyLanguageLearning.Domain.Shared.Kernel;
using System.Collections.Generic;
using System.Linq;

namespace EasyLanguageLearning.Domain.Evaluations.Aggregate
{
    public class WritingExerciseAnswerKey:ValueObject
    {
        public string Heading { get; protected set; }
        protected string answer;
        public WritingExerciseAnswerKey()
        {

        }
        internal WritingExerciseAnswerKey(string heading, string answer)            
        {

            Heading = heading;
            this.answer = answer;
        }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Heading;
            yield return answer;
        }
        public ExerciseOutcome Evaluate(string writtenAnswer)
        {

            if (IsRightAnswer(writtenAnswer))
            {
                return ExerciseOutcome.CreateRightAnswer(writtenAnswer);
            }

            var accuracyPercentage = FigureAccuracyPercentage(writtenAnswer, answer);
            if (accuracyPercentage < 0.5f)
            {
                return ExerciseOutcome.CreateWrongAnswer(answer);
            }
            return ExerciseOutcome.CreateAlmosCorrectAnswer(answer);
        }

        public string GetTip(string partialAnswer)
        {
            var tipIndex = 0;
            var count = 0;
            bool isDiscrepancy = false;
            while (!isDiscrepancy && count < answer.Length)
            {
                if (count < partialAnswer.Length)
                {
                    var nextAnswer = partialAnswer[count];
                    var nextCorrect = answer[count];
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

            return answer.Substring(0, tipIndex + 1);
        }

        private bool IsRightAnswer(string studentAnswer) =>
            studentAnswer == answer;

        private float FigureAccuracyPercentage(string writtenAnswer, string translatedTerm)
        {
            float accuracyCount = translatedTerm.Intersect(writtenAnswer).Count();
            float totalCount = translatedTerm.Count();
            return accuracyCount / totalCount;
        }

        
    }
}
