using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiMediatR
{
    internal class ApplicationDbContext : DbContext
    {
        private static ILoggerFactory loggerFactory =
            LoggerFactory.Create(b => b.AddConsole());
        private readonly IMediator _mediator;

        public ApplicationDbContext(IMediator mediator)
        {
            _mediator = mediator;
        }

        public DbSet<User> Users { get; set; }

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
        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            var domainEntities = ChangeTracker.Entries<IDomainEvents>().Where(x => x.Entity.GetDomainEvents().Any());
            var domainEvents=domainEntities.SelectMany(x=>x.Entity.GetDomainEvents()).ToList();
            domainEntities.ToList().ForEach(x =>x.Entity.ClearDomainEvents());
            foreach (var domainEvent in domainEvents)
            {
                await _mediator.Publish(domainEvent);
            }
            //把消息的发布放到base.SaveChangesAsync()之前，base.SaveChangesAsync()中的代码在同一个事务中，实现强一致性事务
            return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
    }
}
