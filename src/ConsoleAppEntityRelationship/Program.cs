// See https://aka.ms/new-console-template for more information
using ConsoleAppEntityRelationship;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Data.Common;

Console.WriteLine("Hello, World!");
//Get();
//Insert();
//InsertTree();
//Select();
//ExecuteSql();
//await AsyncEnumerate();
Tracking();
static void Insert()
{
    using (ApplicationDbContext db = new ApplicationDbContext())
    {
        db.Articles.Add(new Article
        {
            Title = "文章2",
            Comments = new List<Comment>
            {
                new Comment { Message = "评论3" },
                new Comment { Message = "评论4" }
            }
        });
        db.SaveChanges();
    }
}
static void Get()
{
    var db = new ApplicationDbContext();
    var comment = db.Comments.FirstOrDefault();
    var article = comment.Article;
}
static async void InsertTree()
{

    OrganizationUnit unit = new OrganizationUnit
    {
        Name = "全球总部",
        Children = new List<OrganizationUnit>()
        {
           new OrganizationUnit
           {
               Name ="中国董事会",
               Children=new List<OrganizationUnit>
               {
                   new OrganizationUnit
                   {
                       Name="四川总经理",
                   },
                   new OrganizationUnit
                   {
                       Name="重庆总经理"
                   }
               }
           },
           new OrganizationUnit { Name ="美国董事会"}
        }
    };
    var db = new ApplicationDbContext();
    db.Add(unit);
    db.SaveChanges();
}
static void Select()
{
    ApplicationDbContext db = new ApplicationDbContext();
    var articles = db.Articles.Where(a => a.Comments.Any(c => c.Message.Contains("评论一"))).ToList();
    var articles2=db.Comments.Where(c=>c.Message.Contains("评论3")).Select(c=>c.Article).Distinct().ToList();
}
static async Task AsyncEnumerate()
{
    ApplicationDbContext context = new ApplicationDbContext();
    await foreach (var item in context.Articles.AsAsyncEnumerable())
    {
        Console.WriteLine(item.Title);
    }
}

static async void ExecuteSql()
{
    var name = "michaelshen";
    ApplicationDbContext context = new ApplicationDbContext();
    var sql = "";
    context.Database.ExecuteSqlInterpolated(@$"insert into Articles (Title,Message,Price)
        select Title,{name},Price from Articles");
    FormattableString sqlstr = @$"insert into Articles (Title,Message,Price)
        select Title,{name},Price from Articles";

    IQueryable<Article> articles = context.Articles.FromSqlInterpolated($"select * from Article");

    DbConnection dbConnection = context.Database.GetDbConnection();//获得context对应的底层ADO.NET Core的数据库连接对象.
    if (dbConnection.State != System.Data.ConnectionState.Open)
    {
        dbConnection.Open();
    }
    using(var cmd = dbConnection.CreateCommand())
    {
        cmd.CommandText = "select * from Articles";
        using (var reader= cmd.ExecuteReader())
        {
            while (reader.Read())
            {
                double prize = reader.GetDouble(0);
                int count=reader.GetInt32(1);
            }
        }
    }


}
static void Tracking()
{
    ApplicationDbContext dbContext = new ApplicationDbContext();
    var articles = dbContext.Articles.ToArray();
    var a1 = articles[0];
    a1.Title = "我是新标题";
    EntityEntry e1=dbContext.Entry(a1);
    var state = e1.State;
    Console.WriteLine(e1.DebugView.LongView);
    var articlesNoTracking=dbContext.Articles.AsNoTracking();
    //不推荐这样用
    Article article=new Article { Id=1,Title="a new title"};
    var entry1=dbContext.Entry(article);
    entry1.Property(a => a.Title).IsModified = true;
    dbContext.SaveChanges();
}
static void Filter()
{
    ApplicationDbContext context = new ApplicationDbContext();
    List<Article> articles = context.Articles.IgnoreQueryFilters().Where(a=>a.IsDeleted).ToList();
}