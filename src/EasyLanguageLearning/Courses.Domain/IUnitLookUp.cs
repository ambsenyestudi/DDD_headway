using Courses.Domain.Translations;
using System;
using System.Collections.Generic;

namespace Courses.Domain
{
    public interface IUnitLookUp
    {
        IEnumerable<Unit> GetUnits(Guid courseId);
    }
}
