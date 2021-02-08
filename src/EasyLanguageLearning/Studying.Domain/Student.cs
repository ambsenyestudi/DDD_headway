using System;

namespace Studying.Domain
{
    public class Student
    {

        public Guid Id { get; set; }
        public string Name { get; set; }

        public Student(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        
    }
}
