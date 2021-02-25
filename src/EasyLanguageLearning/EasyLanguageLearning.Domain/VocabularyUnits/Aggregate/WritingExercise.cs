using System;

namespace EasyLanguageLearning.Domain.VocabularyUnits.Aggregate
{
    public class WritingExercise
    {
        public WritingExerciseId Id { get; protected set; }
        public VocabularyId VocabularyId { get; protected set; }
        public bool IsLearningLanguageHeading { get; protected set; }
        public string Heading { get; protected set; }
        public WritingExerciseAnswerKey AnswerKey { get; protected set; }
        protected WritingExercise()
        {
        }
        internal WritingExercise(Guid id, Vocabulary vocabulary, bool isLearningLanguageHeading = false)
        {
            Id = new WritingExerciseId(id);
            VocabularyId = vocabulary.Id;
            IsLearningLanguageHeading = isLearningLanguageHeading;
            Heading = GetHeading(vocabulary);
            AnswerKey = new WritingExerciseAnswerKey(GetHeading(vocabulary), GetAnswer(vocabulary));
        }
        public ExerciseOutcome Evaluate(string writtenAnswer) =>
            AnswerKey.Evaluate(writtenAnswer);

        public string GetTip(string partialAnswer) =>
            AnswerKey.GetTip(partialAnswer);

        private string GetHeading(Vocabulary vocabulary) =>
            IsLearningLanguageHeading
                ? vocabulary.LearningLanguageTerm
                : vocabulary.MotherLanguageTerm;

        private string GetAnswer(Vocabulary vocabulary) =>
            IsLearningLanguageHeading
                ? vocabulary.MotherLanguageTerm
                : vocabulary.LearningLanguageTerm;

    }
}
