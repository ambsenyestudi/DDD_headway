using Courses.Domain;

namespace Studying.Domain
{
    internal class UnitProgression
    {
        public UnitId Unit { get; }
        public int CompletionPercentaje { get; private set; }
        public UnitProgression(UnitId unitId)
        {
            Unit = unitId;
        }
    }
}