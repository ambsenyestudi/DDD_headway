using Courses.Domain.Translations;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Courses.Domain.Exercises
{
    public class MultipleChoiceExercise 
    {
        public const int CHOICE_COUNT = 4;
        private Translation testAnswerTranslation;
        public List<Translation> ChoiceList { get; }
        public MultipleChoiceExercise(Translation testedTranslation, List<Translation> choices)
        {
            testAnswerTranslation = testedTranslation;
            ChoiceList = choices;
            
        }
        /*
        private void SettupMultipleChoce(Guid unitId, ICourseRepository courseRepo)
        {

            var unitContentList = courseRepo.GetUnitContent(unitId);
            Content = unitContentList[GetRandomIndex(0, unitContentList.Count - 1)];

            var max = unitContentList.Count;

            for (int i = 0; i < CHOICE_COUNT; i++)
            {
                var maxVal = max - i;
                if (maxVal < 1)
                {
                    break;
                }
                var curr = unitContentList[GetRandomIndex(0, maxVal)];
                ChoiceList.Add(curr);
                unitContentList.Remove(curr);
            }

            if (!ChoiceList.Contains(Content))
            {
                var index = GetRandomIndex(0, CHOICE_COUNT);
                ChoiceList[index] = Content;
            }
        }
        */
        internal ExerciseResult Evaluate(int answerIndex) =>
            GetAnswerByIndex(answerIndex) == testAnswerTranslation
            ? ExerciseResult.CreateRightAnswer(testAnswerTranslation.TranslatedTerm)
            : ExerciseResult.CreateWrongAnswer(testAnswerTranslation.TranslatedTerm);
            /*
        {
            var answer = ChoiceList[answerIndex];
            if (answer == testAnswerTranslation)
            {
                return ExerciseResult. Create(TestResult.Right, Content);
            }
            return new MultipleChoiceTestResult(TestResult.Wrong, Content);
        }
            */
        private Translation GetAnswerByIndex(int answerIndex) =>
            ChoiceList[answerIndex];
    }
}
