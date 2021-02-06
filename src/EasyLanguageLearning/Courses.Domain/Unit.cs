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
        

        public void LoadContent(List<Translation> contentList)
        {
            content = contentList;
        }

        
    }
}
