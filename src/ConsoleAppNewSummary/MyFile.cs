using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppNewSummary
{
    internal class MyFile : IDisposable
    {
        public void Dispose()
        {
            Console.WriteLine("myfile disposing");
        }
    }
}
