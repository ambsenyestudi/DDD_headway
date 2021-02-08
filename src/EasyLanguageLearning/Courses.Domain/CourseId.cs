using EasyLanguageLearning.Domain.Shared.Kernel;
using System;
using System.Collections.Generic;

namespace Courses.Domain
{
    public class CourseId : ValueObject
    {
        public static CourseId Empty { get; } = new CourseId(Guid.Empty);
        public Guid Value { get; }
        public CourseId(Guid id)
        {
            Value = id;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
