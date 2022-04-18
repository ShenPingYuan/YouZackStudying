using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppNewSummary
{
    internal class Student
    {

        //public string? Name { get; set; }
        //public string? PhoneNumber { get; set; }
        public string Name { get; set; }
        public string? PhoneNumber { get; set; }
        public int Age { get; init; }
        public Student(string name)
        {
            Name = name;
        }
        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
