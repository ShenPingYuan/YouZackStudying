// See https://aka.ms/new-console-template for more information
using Linq1;

Console.WriteLine("Hello, World!");

int[] nums = new int[] { 1, 4, 5, 634, 754, 34, 87, 3, 8, 9, 56, 756 };
IEnumerable<int> result = nums.Where(x => x > 100);
foreach (var item in result)
{
    Console.WriteLine(item);
}

IEnumerable<int> result2 = nums.MyWhere(x => x > 100);
foreach (var item in result2)
{
    Console.WriteLine(item);
}
Array.Sort(nums);
//随机排序
var result3=nums.OrderBy(x=>Guid.NewGuid());
Random rnd = new Random();
var result4 = nums.OrderBy(x => rnd.Next());


string s = "hello,world var result4 = nums.OrderBy(x => rnd.Next());";
IEnumerable<char>? chars = s.Where(c => char.IsLetter(c));
var result5 = chars.Select(c => char.ToUpper(c)).GroupBy(c => c).Select(c => new { c.Key, Count = c.Count() }).Where(g => g.Count > 2).OrderByDescending(g=>g.Count);
foreach (var item in result5)
{
    Console.WriteLine(item.Key+" : "+item.Count);
}