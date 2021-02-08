using Courses.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Studying.Domain
{
    public interface ICouserLookup
    {
        List<UnitId> GetUnits(CourseId courseId);
    }
}
