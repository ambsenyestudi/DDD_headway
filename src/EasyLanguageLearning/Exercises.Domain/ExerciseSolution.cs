using EasyLanguageLearning.Domain.Shared.Kernel.Units;

namespace Exercises.Domain
{
    public class ExerciseSolution
    {
        public UnitContentItem StudenSolution{ get; }
        public ExerciseSolution(UnitContentItem studentSolution)
        {
            StudenSolution = studentSolution;
        }
    }
}
