using EasyLanguageLearning.Domain.Shared.Kernel.Units;
using System.Linq;

namespace Exercises.Domain
{
    public class WrittingExercise : Exercise
    {
        bool isLearningLanguage;
        private string TestAnswer 
        {
            get =>
                isLearningLanguage
                ? RightSolution.MotherLangugeTerm 
                : RightSolution.LearningLanguageTerm; 
        }
        public WrittingExercise(UnitContentItem rightSolution, bool isLearningLanguage = false) 
            : base(
                  isLearningLanguage 
                  ? rightSolution.LearningLanguageTerm
                  : rightSolution.MotherLangugeTerm, 
                  rightSolution)
        {
            this.isLearningLanguage = isLearningLanguage;
        }
        public ExerciseOutcome Evaluate(string writtenAnswer)
        {
            
            if (writtenAnswer == TestAnswer)
            {
                return ExerciseOutcome.CreateRightAnswer(TestAnswer);
            }

            var accuracyPercentage = FigureAccuracyPercentage(writtenAnswer, TestAnswer);
            if (accuracyPercentage < 0.5f)
            {
                return ExerciseOutcome.CreateWrongAnswer(TestAnswer);
            }
            return ExerciseOutcome.CreateAlmosCorrectAnswer(TestAnswer);
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
