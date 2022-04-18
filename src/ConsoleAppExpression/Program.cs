// See https://aka.ms/new-console-template for more information
using ConsoleAppExpression;
using ExpressionTreeToString;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using static System.Linq.Expressions.Expression;

Console.WriteLine("Hello, World!");
Expression<Func<Book, bool>> expression = b => b.Price > 5;
Expression<Func<Book, Book, double>> expression2 = (b1, b2) => b1.Price + b2.Price;
Expression<Func<Book, bool>> expression3 = b => b.Price > 5 && b.Title.Contains("michaelshen");

Func<Book, bool> func1 = b => b.Price > 5;
Func<Book, bool> func2 = b => { return b.Price > 5; };//正确
//Expression<Func<Book, bool>> expression5 = b => { return b.Price > 5; };//错误
ApplicationDbContext db = new ApplicationDbContext();
//db.Books.Where(expression);//IQueryable
//db.Books.Where(func1);//IEnumerable

IQueryable<object[]> queryable = db.Books.Select(x=>new object[] { x.Id,x.Title});

Console.WriteLine(expression.ToString("Object notation", "C#"));
Console.WriteLine(expression.ToString("Factory Methods", "C#"));
Console.WriteLine(expression3.ToString("Object notation", "C#"));
Console.WriteLine(expression3.ToString("Factory Methods", "C#"));

//ParameterExpression、BinaryExpression、MethodCallExpression、ConstantExpress
ParameterExpression paramExpreB = Expression.Parameter(typeof(Book), "b");
ConstantExpression constantExpr5 = Expression.Constant(5.0, typeof(double));
MemberExpression memberExprePrice = Expression.MakeMemberAccess(paramExpreB, typeof(Book).GetProperty("Price"));
BinaryExpression binaryExpressionGreaterThan = Expression.GreaterThan(memberExprePrice, constantExpr5);
Expression<Func<Book, bool>> lambdaExpression = Expression.Lambda<Func<Book, bool>>(binaryExpressionGreaterThan, paramExpreB);

var b = Expression.Parameter(
    typeof(Book),
    "b"
);

var expression5 = Lambda<Func<Book, bool>>(
    AndAlso(
        GreaterThan(
            MakeMemberAccess(b,
                typeof(Book).GetProperty("Price")
            ),
            Constant(5.0)
        ),
        Call(
            MakeMemberAccess(b,
                typeof(Book).GetProperty("Title")
            ),
            typeof(string).GetMethod("Contains", new[] { typeof(string) }),
            Constant("michaelshen")
        )
    ),
    b
);



db.Books.Where("Title=='michaelshen' and Id>0").OrderBy("Id");