// See https://aka.ms/new-console-template for more information
using ConsoleAppEFCore充血模型;

Console.WriteLine("Hello, World!");

//Students();
Console.WriteLine("hell,world");

static void Users()
{
    using var context = new ApplicationDbContext();
    //User u1 = new User("spy");
    //u1.ChangePassword("123456");
    //context.Users.Add(u1);
    //context.SaveChanges();

    User? user2 = context.Users.First();
}
static void Students()
{
    using var context = new ApplicationDbContext();
    Student student = new Student()
    {
        Name = "michaenshen2",
        StudentType = StudentType.CENTER,
        Location = new Geo(20, 120)
    };
    
    context.Students.Add(student);
    context.SaveChanges();

    Student? studentInDb=context.Students.First(s=>s.StudentType==StudentType.CENTER);
}
 