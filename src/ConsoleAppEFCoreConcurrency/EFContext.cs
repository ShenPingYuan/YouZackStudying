using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppEFCoreConcurrency
{
    internal class EFContext:DbContext
    {
        public DbSet<House> Houses { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            string sqlConnectionString = "Database=EFdemo;server=94.191.83.150;Port=3306;User=root;Password=2439739932";
            var version = new MySqlServerVersion(new Version(8, 0, 28));
            optionsBuilder.UseMySql(sqlConnectionString,version);
            optionsBuilder.LogTo(msg =>
            {
                if (msg.Contains("CommandExecuted"))
                    Console.WriteLine(msg);
            });
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);//从当前程序集加载配置
        }
    }
}
