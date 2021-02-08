using EasyLanguageLearning.Domain.Shared.Kernel.Units;
using System.Collections.Generic;

namespace Exercises.Domain
{
    public class MultipleChoiceExercise : Exercise
    {
        public const int CHOICE_COUNT = 4;
        public List<UnitContentItem> ChoiceList { get; }

        public MultipleChoiceExercise(int rightAnswerIndex, List<UnitContentItem> choices, bool isLearningLanguage = false) : 
            base(isLearningLanguage
                ? choices[rightAnswerIndex].LearningLanguageTerm
                : choices[rightAnswerIndex].MotherLangugeTerm,
                choices[rightAnswerIndex])
        {
            ChoiceList = choices;
        }

        //Domain experts count from 1 to 4
        public ExerciseOutcome Evaluate(int answerIndex) =>
             GetAnswerByIndex(answerIndex - 1) == RightSolution
             ? ExerciseOutcome.CreateRightAnswer(RightSolution.LearningLanguageTerm)
             : ExerciseOutcome.CreateWrongAnswer(RightSolution.LearningLanguageTerm);

        private UnitContentItem GetAnswerByIndex(int answerIndex) =>
            ChoiceList[answerIndex];
    }
}
