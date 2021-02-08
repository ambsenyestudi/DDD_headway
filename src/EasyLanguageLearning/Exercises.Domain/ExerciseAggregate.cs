using EasyLanguageLearning.Domain.Shared.Kernel.Exercises;
using EasyLanguageLearning.Domain.Shared.Kernel.Units;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Exercises.Domain
{
    public class ExerciseAggregate
    {
        public WrittingExercise CreateWrittingExercise(UnitContentItem content, bool isLearningLanguage = false)
        {
            EnsureContent(content);
            return new WrittingExercise(content, isLearningLanguage);
        }

        public ExerciseResult EvaluateWrittingExercise(WrittingExercise exercise, string studentSolution)
        {
            var outcome = exercise.Evaluate(studentSolution);
            return ExerciseResult.Create(outcome.CorrectAnswer, outcome.Result);
        }
        public string GetTip(WrittingExercise exercise, string partialAnswer) =>
            exercise.GetTip(partialAnswer);

        public MultipleChoiceExercise CreateMultipleChoiceExercise(int rightAnswerIndex, List<UnitContentItem> choiceList, bool isLearningLanguage = false)
        {
            EnsureMulitpleChoice(choiceList);
            return new MultipleChoiceExercise(rightAnswerIndex, choiceList, isLearningLanguage);
        }

        public ExerciseResult EvaluateMultipleChoiceExercise(MultipleChoiceExercise exercise, int answerIndex)
        {
            var outcome = exercise.Evaluate(answerIndex);
            return ExerciseResult.Create(outcome.CorrectAnswer, outcome.Result);
        }
        public void EnsureContent(UnitContentItem contentItem)
        {
            if(contentItem.Equals(UnitContentItem.Empty))
            {
                throw new ArgumentException("Exercice with no content");
            }
        }
        public void EnsureMulitpleChoice(List<UnitContentItem> choiceList)
        {
            if(choiceList == null || !choiceList.Any())
            {
                throw new ArgumentException("Multiple choice exercise has no choices");
            }
            if(choiceList.Count!= MultipleChoiceExercise.CHOICE_COUNT)
            {
                throw new ArgumentException("Multiple choice exercise invalid number of options");
            }
        }
    }
}
