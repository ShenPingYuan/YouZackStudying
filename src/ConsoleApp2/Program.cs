// See https://aka.ms/new-console-template for more information

using System.Text;

Console.WriteLine(Thread.CurrentThread.ManagedThreadId);

StringBuilder sb = new StringBuilder();
for (int i = 0; i < 10000; i++)
{
    sb.Append("XXXXXXXXXXXXXXXXXXXXXXXXXXXX");
}
await File.WriteAllTextAsync(@"./2.txt", sb.ToString());
Console.WriteLine(Thread.CurrentThread.ManagedThreadId);


Console.WriteLine("CalAsync Starting..." + Thread.CurrentThread.ManagedThreadId);
await CalcAsync(5000);
Console.WriteLine("CalAsync ending..." + Thread.CurrentThread.ManagedThreadId);
Console.WriteLine("CalAsync2 Starting..." + Thread.CurrentThread.ManagedThreadId);
await CalcAsync(5000);
Console.WriteLine("CalAsync2 ending..." + Thread.CurrentThread.ManagedThreadId);
static async Task<decimal> CalcAsync(int n)
{
    /*
    Console.WriteLine("CalcAsync-ThreadId:"+Thread.CurrentThread.ManagedThreadId);
    decimal result = 1;
    Random random = new Random();
    for (int i = 0; i < n*n; i++)
    {
        result += (decimal)random.NextDouble();
    }
    return result;
    */

    return await Task.Run(() =>
    {
        Console.WriteLine("CalcAsync-ThreadId:" + Thread.CurrentThread.ManagedThreadId);
        decimal result = 1;
        Random random = new Random();
        for (int i = 0; i < n * n; i++)
        {
            result += (decimal)random.NextDouble();
        }
        return result;
    });
}

