using Courses.Domain.Exceptions;
using Courses.Domain.Translations;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Courses.Domain
{
    public class Unit
    {
        public Guid Id { get; }
        public string Name { get; }
        private IEnumerable<Translation> content;
        public List<Translation> Content { get=> content.ToList(); }

        public Unit(Guid id, string name)
        {
            this.Id = id;
            this.Name = name;
        }
        

        internal void LoadContent(List<Translation> contentList)
        {
            EnsureNotRepeatedContent(contentList);
            content = contentList;
        }
        internal List<Translation> GetRandomContent(int max, bool isFixedRandom = false)
        {
            var randomContentList = new List<Translation>();
            var unitContent = Content;
            var random = new Random();
            for (int i = 0; i < max; i++)
            {
                var randomIndex = isFixedRandom
                    ? 0
                    :random.Next(0, unitContent.Count - 1);

                randomContentList.Add(unitContent[randomIndex]);
                unitContent.RemoveAt(randomIndex);
            }
            return randomContentList;
        }

        private void EnsureNotRepeatedContent(List<Translation> contentList)
        {
            int dupes = contentList.Count() - contentList.GroupBy(x=>x).Count();
            if (dupes > 0)
            {
                throw new RepeatedUnitContentException(RepeatedUnitContentException.REPEATED_CONTENT_ERROR);
            }
        }
    }
}
