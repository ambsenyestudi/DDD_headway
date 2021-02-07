using EasyLanguageLearning.Domain.Shared.Kernel;

namespace Courses.Domain.Exceptions
{
    public class InvalidUnitContentException: DomainException
    {
        public const string INVALID_CONTENT_LANGUAGE_ERROR = "Unit invalid language translations";
        public InvalidUnitContentException(string message):base(message)
        {
        }
    }
}
