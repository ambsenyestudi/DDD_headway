using System;
using System.Collections.Generic;

namespace EasyLanguageLearning.Domain.Shared.Kernel.Units
{
    public class UnitContentItemId: ValueObject
    {
        public Guid Value { get; }
        public static UnitContentItemId Empty { get; } = new UnitContentItemId(Guid.Empty);
        protected UnitContentItemId(Guid value)
        {
            Value = value;
        }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
