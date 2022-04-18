namespace WebApplicationHostedServices
{
    public class MyHostedService : BackgroundService
    {
        private readonly IServiceScope _serviceScope;

        public MyHostedService(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScope = serviceScopeFactory.CreateScope();
        }

        //从.Net6开始如果托管服务中出现异常，程序就会自动停止并退出
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            ILogger? logger = _serviceScope.ServiceProvider.GetService<ILogger>();
            Console.WriteLine("启动后台服务");
            await Task.Delay(3000);//不要用Sleep
            string txt =await File.ReadAllTextAsync("./1.txt");
            Console.WriteLine("文件读取完成");
            await Task.Delay(3000);
            Console.WriteLine(txt);
        }
        public override void Dispose()
        {
            _serviceScope.Dispose();
            base.Dispose();
        }
        //定时执行框架：Hangfire
    }
}
