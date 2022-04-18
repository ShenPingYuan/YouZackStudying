// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

foreach (var item in test())
{
    Console.WriteLine(item);
}
static IEnumerable<string> test()
{
    yield return "I";
    yield return "Love";
    yield return "You";
}
static async IAsyncEnumerable<string> test2()
{
    await Task.Delay(1000);
    yield return "I";
    yield return "Love";
    yield return "You";
}