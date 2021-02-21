using EasyLanguageLearning.Domain.Shared.Kernel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EasyLanguageLearning.Domain.LearningPaths.Aggregate
{
    public class Level: ValueObject
    {
        public int Value { get; }
        public static Level Empty { get; } = new Level(0);
        public static Level First { get; } = new Level(1);

        private Level(int level)
        {
            Value = level;
        }
        public bool IsNextLevel(Level next) =>
            next.Value - 1 == Value;

        public override string ToString() => "" + Value;

        public static Level Create(int level) =>
            IsPositiveLevel(level)
            ? new Level(level)
            : Level.Empty;

        public static void EnsurePostiveLevel(int level)
        {
            if (!IsPositiveLevel(level))
            {
                throw new ArgumentException($"{nameof(Level)} not postive");
            }
        }

        public void EnsureNotRepeated(IList<Level> levelCollection)
        {
            if(this.IsInList(levelCollection))
            {
                throw new ArgumentException($"Repetated {nameof(Level)}e");
            }
        }

        public void EnsureOrderlyFashon(IList<Level> levelCollection)
        {
            if (levelCollection.Any())
            {
                var previousLevel = levelCollection.Last();
                if (!previousLevel.IsNextLevel(this))
                {
                    throw new ArgumentException($"Levels must be ordered instead of {previousLevel}, {this}");
                }
            }
            else if (this != Level.First)
            {
                throw new ArgumentException($"Course levels start at {Level.First}");
            }
        }

        public bool IsInList(IList<Level> levelCollection) => 
            levelCollection.Contains(this);

        public static bool IsPositiveLevel(int level) => 
            level > 0;

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
