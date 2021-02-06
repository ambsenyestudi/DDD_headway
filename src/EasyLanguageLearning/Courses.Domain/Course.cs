namespace Courses.Domain
{
    public class Course
    {
        private readonly Translator translator;

        public string Name { get; }
        public Course(Translator translator, string languageName)
        {
            this.translator = translator;
            Name = translator.Translate(languageName);
        }

    }
}
