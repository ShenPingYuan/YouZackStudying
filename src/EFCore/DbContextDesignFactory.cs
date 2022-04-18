using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace EFCore
{
    public class DbContextDesignFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        //开发时运行,如果把这个项目设置为启动项目的话，这样迁移时就不知道使用那个数据库，所以需要配置一下，如果把
        //api项目设置为启动项，就不需要这个
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var optionBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            string sqlConnection = "Server =.; Database = webapidemo; Trusted_Connection = true; MultipleActiveResultSets = true";
            optionBuilder.UseSqlServer(sqlConnection);
            return new ApplicationDbContext(optionBuilder.Options);
        }
    }
}
