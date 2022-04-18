using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemServices
{
    internal class Test2
    {
        private readonly ILogger<Test2> _logger;

        public Test2(ILogger<Test2> logger)
        {
            _logger = logger;
        }
        public void Test()
        {
            _logger.LogDebug("开始执行FTP同步");
            _logger.LogDebug("开始连接FTP成功");
            _logger.LogWarning("查找FTP失败，重试一次");
            _logger.LogWarning("查找FTP失败，重试二次");
            _logger.LogError("查找FTP最终失败");
            _logger.LogInformation("注册一个用户{@person}",new User { Age = 1,Name="michaelshen" });
            try
            {
                File.ReadAllText("./2.txt");
                _logger.LogDebug("读取文件成功");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "读取文件失败");
            }
        }
    }
    public class User
    {
        public string? Name { get; set; }
        public int Age { get; set; }
    }
}
