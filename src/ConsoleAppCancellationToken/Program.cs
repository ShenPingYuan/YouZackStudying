// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

CancellationTokenSource cts = new CancellationTokenSource();
//cts.CancelAfter(TimeSpan.FromSeconds(5));

DownloadAsync("https://www.baidu.com", 100,cts.Token);
while (Console.ReadLine() != "q")
{

}
cts.Cancel();
Console.ReadLine();
static async Task DownloadAsync(string url, int n,CancellationToken cancellation)
{
    using (HttpClient client = new HttpClient())
    {
        for (int i = 0; i < n; i++)
        {
            await client.GetStringAsync(url);
            Console.WriteLine($"{DateTime.Now}");
            if (cancellation.IsCancellationRequested)
            {
                Console.WriteLine("请求被取消");
                break;
            }
            //cancellation.ThrowIfCancellationRequested();
        }
    }
}