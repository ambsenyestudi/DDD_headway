using Courses.Domain.Translations;
using System.Collections.Generic;

namespace Courses.Domain.Exercises
{
    public class MultipleChoiceExercise 
    {
        public const int CHOICE_COUNT = 4;
        private Translation testAnswerTranslation;
        public List<Translation> ChoiceList { get; }

        public MultipleChoiceExercise(int answerIndex, List<Translation> choices)
        {
            testAnswerTranslation = choices[answerIndex];
            ChoiceList = choices;
            
        }

       //Domain experts count from 1 to 4
       public ExerciseResult Evaluate(int answerIndex) =>
            GetAnswerByIndex(answerIndex - 1) == testAnswerTranslation
            ? ExerciseResult.CreateRightAnswer(testAnswerTranslation.TranslatedTerm)
            : ExerciseResult.CreateWrongAnswer(testAnswerTranslation.TranslatedTerm);

        private Translation GetAnswerByIndex(int answerIndex) =>
            ChoiceList[answerIndex];

    }
}
