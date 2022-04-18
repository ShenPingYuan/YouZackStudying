using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppEFCore
{
    internal class ApplicationDbContext : DbContext
    {
        private static ILoggerFactory loggerFactory =
            LoggerFactory.Create(b => b.AddConsole());
        public DbSet<Book> Books { get; set; }
        public DbSet<Person> Persons { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            string sqlConnectionString = "Server=.;Database=efcoreDemo;Trusted_Connection=true;MultipleActiveResultSets=true";
            optionsBuilder.UseSqlServer(sqlConnectionString);
            //method 1
            //optionsBuilder.UseLoggerFactory(loggerFactory);
            //method 2
            optionsBuilder.LogTo(msg =>
            {
                if (msg.Contains("CommandExecuted"))
                    Console.WriteLine(msg);
            });
            //optionsBuilder.LogTo(Console.WriteLine);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);//从当前程序集加载配置
        }
    }
}
