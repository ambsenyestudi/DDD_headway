using EasyLanguageLearning.Domain.Shared.Kernel;

namespace Courses.Domain.Exceptions
{
    public class RepeatedUnitContentException: DomainException
    {
        public const string REPEATED_CONTENT_ERROR = "Unit has some repreated content";
        public RepeatedUnitContentException(string message): base(message)
        {
        }
    }
}
