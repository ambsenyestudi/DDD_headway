using System.Linq;

namespace EasyLanguageLearning.Domain.VocabularyUnits.Aggregate
{
    public class WritingExercise
    {
        public string Heading { get; protected set; }
        protected string Answer { get; set; }

        internal WritingExercise(TranslatedContent rightSolution, bool isLearningLanguageHeading = false)            
        {
            
            Heading = GetHeading(rightSolution, isLearningLanguageHeading);
            Answer = GetAnswer(rightSolution, isLearningLanguageHeading);
        }

        //todo test evaluate method
        public ExerciseOutcome Evaluate(string writtenAnswer)
        {

            if (IsRightAnswer(writtenAnswer))
            {
                return ExerciseOutcome.CreateRightAnswer(writtenAnswer);
            }

            var accuracyPercentage = FigureAccuracyPercentage(writtenAnswer, Answer);
            if (accuracyPercentage < 0.5f)
            {
                return ExerciseOutcome.CreateWrongAnswer(Answer);
            }
            return ExerciseOutcome.CreateAlmosCorrectAnswer(Answer);
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

            return Answer.Substring(0, tipIndex + 1);

        }
        private bool IsRightAnswer(string studentAnswer) =>
            studentAnswer == Answer;
        private string GetHeading(TranslatedContent content, bool isLearningHeading) =>
            isLearningHeading
                ? content.LearningLanguageTerm
                : content.MotherLanguageTerm;

        private string GetAnswer(TranslatedContent content, bool isLearningHeading) =>
            isLearningHeading
                ? content.MotherLanguageTerm
                : content.LearningLanguageTerm;
        private float FigureAccuracyPercentage(string writtenAnswer, string translatedTerm)
        {
            float accuracyCount = translatedTerm.Intersect(writtenAnswer).Count();
            float totalCount = translatedTerm.Count();
            return accuracyCount / totalCount;
        }

    }
}
