using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIExample
{
    internal class Student : IStudent
    {
        public Student()
        {

        }
        public string Name { get ; set; }

        public void Say()
        {
            Console.WriteLine("My name is "+Name);
        }
    }
}
