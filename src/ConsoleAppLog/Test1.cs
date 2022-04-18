using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppLog
{
    internal class Test1
    {
        private readonly ILogger<Test1> _logger;

        public Test1(ILogger<Test1> logger)
        {
            _logger = logger;
        }
        public void Test()
        {
            _logger.LogDebug("开始执行数据库同步");
            _logger.LogDebug("开始连接数据库成功");
            _logger.LogWarning("查找数据库失败，重试一次");
            _logger.LogWarning("查找数据库失败，重试二次");
            _logger.LogError("查找数据库最终失败");
            try
            {
                File.ReadAllText("./2.txt");
                _logger.LogDebug("读取文件成功");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,"读取文件失败");
            }
        }
    }
}
