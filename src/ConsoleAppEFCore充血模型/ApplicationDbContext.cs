using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppEFCore充血模型
{
    internal class ApplicationDbContext : DbContext
    {
        private static ILoggerFactory loggerFactory =
            LoggerFactory.Create(b => b.AddConsole());
        public DbSet<User> Users { get; set; }
        public DbSet<Student> Students { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            string sqlConnectionString = "Server=.;Database=DDD1;Trusted_Connection=true;MultipleActiveResultSets=true";
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
