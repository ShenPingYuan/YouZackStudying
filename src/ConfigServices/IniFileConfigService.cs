using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigServices
{
    public class IniFileConfigService : IConfigService
    {
        public string FilePath { get; set; }
        public string GetValue(string name)
        {
            Console.WriteLine(FilePath);
            return FilePath;
        }
    }
}
