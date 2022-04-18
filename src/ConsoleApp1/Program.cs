// See https://aka.ms/new-console-template for more information
//await Fun();
string s = await DownLoadHtmlAsync("https://www.baidu.com", @".\htmlstr.txt");
static async Task Fun()
{
    string fileName = @".\1.txt";
    //File.WriteAllText(fileName, "hello");
    //string s=File.ReadAllText(fileName);
    //Console.WriteLine(s);

    await File.WriteAllTextAsync(fileName, "hello async");

    string s = File.ReadAllText(fileName);
    Console.WriteLine(s);

}

static async Task<string> DownLoadHtmlAsync(string url, string filename)
{
    using (HttpClient client = new HttpClient())
    {
        string htmlString=await client.GetStringAsync(url);
        await File.WriteAllTextAsync(filename, htmlString);
        //return htmlString;
    }
    string fileName2 = @".\1.txt";
    await File.WriteAllTextAsync(fileName2, "hello async");
    return "完成";

}