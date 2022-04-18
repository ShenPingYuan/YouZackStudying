using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppEFCore充血模型
{
    internal class User 
    {
        public int Id { get; init; }
        public DateTime CreateTime { get; init; }
        public string  UserName { get;private set; }
        public int Credit { get; private set; }
        private string? passwordHash;
        public string? remark;
        public string? Remark
        {
            get { return remark; }
        }
        public string? Tag { get; set; }
        public StudentType StudentType { get; set; }
        private User()//给EFCore从数据库读出数据用的
        {
        }
        public User(string yhm)//我们自己用
        {
            UserName = yhm;
            CreateTime = DateTime.Now;
            Credit = 10;
        }
        public void ChangeUserName(string un)
        {
            if (un.Length > 5||un.Length<1)
            {
                Console.WriteLine("用户长度不能大于5");
                return;
            }
            UserName = un;
        }
        public void ChangePassword(string pwd)
        {
            if (pwd.Length != 0)
            {
                passwordHash= "HASH"+pwd;
            }
        }

        
    }
}
