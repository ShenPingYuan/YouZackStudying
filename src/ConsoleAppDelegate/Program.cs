// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");
AddDelegate ad = Add;
Console.WriteLine(ad(2,3));

Func<int, int, int> func = Add;
Func<int,int,int> func2=(a,b)=> { return a+b;};
static int Add(int x, int y)
{
    return x + y;
}

delegate int AddDelegate(int x, int y);