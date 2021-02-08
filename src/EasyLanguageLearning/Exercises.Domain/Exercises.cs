using EasyLanguageLearning.Domain.Shared.Kernel.Units;

namespace Exercises.Domain
{
    public class Exercises
    {
        public string Heading { get; }
        public UnitContentItem RightSolution{ get; }
        public Exercises(string heading, UnitContentItem rightSolution)
        {
            Heading = heading;
        }


    }
}
