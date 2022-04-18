using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppEntityRelationship
{
    internal class ApplicationDbContext:DbContext
    {
        public DbSet<Article> Articles { get; set; }
        public DbSet<Comment> Comments{ get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Leave> Leaves { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Student> Students { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            string sqlConnectionString = "Server=.;Database=efcoreRelationship;Trusted_Connection=true;MultipleActiveResultSets=true";
            optionsBuilder.UseSqlServer(sqlConnectionString);
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
