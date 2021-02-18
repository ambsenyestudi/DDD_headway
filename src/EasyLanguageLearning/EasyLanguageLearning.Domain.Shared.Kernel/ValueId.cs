using System;
using System.Collections.Generic;

namespace EasyLanguageLearning.Domain.Shared.Kernel
{
    public class ValueId: ValueObject
    {
        public Guid Value { get; }
        public ValueId(Guid id)
        {
            Value = id;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
