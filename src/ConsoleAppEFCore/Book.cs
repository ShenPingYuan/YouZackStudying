using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppEFCore
{
    internal class Book
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public DateTime PublicTime { get; set; }
        public double Price { get; set; }
    }
}
