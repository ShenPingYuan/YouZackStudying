using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StandardLibraryTest
{
    public class TestClass
    {
        public static void test()
        {
            Console.WriteLine(typeof(FileStream).Assembly.Location);
            Console.WriteLine(typeof(TestClass).Assembly.Location);
            Console.ReadLine();
        }
    }
}
