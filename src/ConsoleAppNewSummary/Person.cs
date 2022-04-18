using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppNewSummary
{
    internal record Person(int Id,string Name,int Age);//属性只读
    internal record Person2(int Id,string Name)//Id、Name只读
    {
        public int Age { get; set; }//Age可读可写
    }
    internal record Person3()
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Age { get; set; }
    }
    internal record Person4(int Id, string Name)//Id、Name只读
    {
        public Person4(int Id, string Name,int Age):this(Id,Name)
        {
            this.Age = Age;
            this.Name = Name;
            this.Id = Id;
        }
        public int Age { get; set; }//Age可读可写
    }
}
