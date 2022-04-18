using System;
using System.Collections.Generic;
using System.Text;

namespace LogServices
{
    class ConsoleLogProvider : ILogProvider
    {
        public void LogError(string message)
        {
            Console.WriteLine($"ERROR:{message}");
        }

        public void LogInfo(string message)
        {
            Console.WriteLine($"Info:{message}");
        }
    }
}
