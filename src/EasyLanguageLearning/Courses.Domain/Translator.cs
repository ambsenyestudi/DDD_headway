using EasyLanguageLearning.Domain.Shared.Kernel.Languages;
using System.Collections.Generic;

namespace Courses.Domain
{
    public class Translator
    {
        public Iso MotherLanguageIso { get; }
        public Iso LeaningLanguageIso { get; }

        private Dictionary<string, string> translationDictionary = new Dictionary<string, string>();
        public Translator(Iso motherIso, Iso learningIso)
        {
            MotherLanguageIso = motherIso;
            LeaningLanguageIso = learningIso;
            translationDictionary.Add("English", "Inglés");
        }
        public string Translate(string term) =>
            translationDictionary[term];
    }
}
