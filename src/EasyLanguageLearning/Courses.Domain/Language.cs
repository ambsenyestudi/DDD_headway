using EasyLanguageLearning.Domain.Shared.Kernel;
using EasyLanguageLearning.Domain.Shared.Kernel.Languages;
using System.Collections.Generic;

namespace Courses.Domain
{
    public class Language : ValueObject
    {
        public static Language Empty { get; } = new Language(string.Empty, Iso.Empty);
        public string Name { get; }
        public Iso Iso { get; }
        protected Language(string name, Iso iso)
        {
            Name = name;
            Iso = iso;
        }
        public static Language CreateFromNameAndIso(string name, Iso iso)
        {
            if(string.IsNullOrWhiteSpace(name) || iso == Iso.Empty)
            {
                return Empty;
            }
            return new Language(name, iso);
        }

        protected override IEnumerable<object> GetEqualityComponents() =>
            new object[] { Name, Iso };
    }
}
