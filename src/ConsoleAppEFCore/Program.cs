// See https://aka.ms/new-console-template for more information
using ConsoleAppEFCore;
using Microsoft.EntityFrameworkCore;
using System.Linq;

using (ApplicationDbContext context = new ApplicationDbContext())
{
    Person person;
    int age = 18;
    for (int i = 0; i < 10; i++)
    {
        person = new Person
        {
            Name = "michael shen",
            Age = age++,
        };
        context.Add(person);
    }
    context.SaveChanges();

    var persons = context.Persons;
    var personsQuery = persons.Where(p => p.Name == "michael shen");
    string sqlString= personsQuery.ToQueryString();
    //persons.ToList().ForEach(person =>
    //{
    //    person.Age++;
    //});
    //context.SaveChanges();
    //foreach (Person item in persons)
    //{
    //    Console.WriteLine(item.Name+item.Age);
    //}
}