using System.Collections.Generic;
using System.Linq;

namespace EasyLanguageLearning.Domain.VocabularyUnits.Aggregate
{
    internal class VocabularyCollection
    {
        private List<Vocabulary> items = new List<Vocabulary>();
        public bool IsEmpty { get => !items.Any(); }
        public IEnumerable<Vocabulary> ListVocabulary()=>
            items.AsEnumerable();

        public void Add(Vocabulary vocabulary) =>
            items.Add(vocabulary);

        public IEnumerable<Vocabulary> GetLearningTermStartsBy(string start) =>
            items.Where(v => v.LearningLanguageTerm.StartsWith(start));
    }
}
