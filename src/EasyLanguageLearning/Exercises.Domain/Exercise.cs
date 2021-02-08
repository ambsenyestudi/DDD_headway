using EasyLanguageLearning.Domain.Shared.Kernel.Units;

namespace Exercises.Domain
{
    public class Exercise
    {
        public string Heading { get; }
        public UnitContentItem RightSolution{ get; }
        public Exercise(string heading, UnitContentItem rightSolution)
        {
            Heading = heading;
            RightSolution = rightSolution;
        }
        

    }
}
