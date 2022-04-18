// See https://aka.ms/new-console-template for more information
using ConsoleAppEFCoreConcurrency;
using Microsoft.EntityFrameworkCore;

Console.WriteLine("Hello, World!");
Concurrency2();

static void Concurrency1()
{
    Console.WriteLine("请输入你的名字");
    string name = Console.ReadLine();

    EFContext context = new EFContext();
    using (var tx = context.Database.BeginTransaction())
    {

        //var houseInDb = context.Houses.Single(h => h.Id == 1);
        var houseInDb = context.Houses.FromSqlInterpolated($"select * from Houses where Id=1 for update").Single();
        if (!string.IsNullOrEmpty(houseInDb.Owner))
        {
            if (houseInDb.Owner == name)
            {
                Console.WriteLine("房子已经被你抢到了");
            }
            else
            {
                Console.WriteLine($"房子已经被:'{houseInDb.Owner}'占了");
            }
            Console.ReadLine();
            return;
        }
        houseInDb.Owner = name;
        Thread.Sleep(5000);
        context.SaveChanges();
        Console.WriteLine("恭喜你，抢到了");
        tx.Commit();
    }
    Console.ReadLine();
}
static async void Concurrency2()
{
    EFContext context = new EFContext();
    var houseInDb = context.Houses.Single();
    Console.WriteLine("请输入你的名字");
    string name = Console.ReadLine();
    if (!string.IsNullOrEmpty(houseInDb.Owner))
    {
        if (houseInDb.Owner == name)
        {
            Console.WriteLine("房子已经被你抢到了");
        }
        else
        {
            Console.WriteLine($"房子已经被:'{houseInDb.Owner}'占了");
        }
        Console.ReadLine();
        return;
    }
    houseInDb.Owner = name;
    Thread.Sleep(5000);
    try
    {
        context.SaveChanges();
        Console.WriteLine("恭喜你，抢到了");
    }
    catch (DbUpdateConcurrencyException ex)
    {
        var entry=ex.Entries.First();
        var valueInDb = entry.GetDatabaseValues().GetValue<string>("Owner");
        var newValue = entry.CurrentValues["Owner"];
        //var dbValues=await entry.GetDatabaseValuesAsync();
        //string newOwner = dbValues.GetValue<string>(nameof(House.Owner));
        //Console.WriteLine($"并发冲突，房子被{newOwner}提前抢走了");
        Console.WriteLine($"并发冲突,房子被{valueInDb}抢走了");
        //ex.
    }
    Console.ReadLine();
}