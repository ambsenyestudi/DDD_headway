using EasyLanguageLearning.Domain.Shared.Kernel;
using System;
using System.Collections.Generic;

namespace Courses.Domain
{
    public class UnitId : ValueObject
    {
        public Guid Value { get; }
        public static UnitId Empty { get; } = new UnitId(Guid.Empty);

        public UnitId(Guid id)
        {
            Value = id;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
