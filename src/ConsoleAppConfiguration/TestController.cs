using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppConfiguration
{
    internal class TestController
    {
        private readonly IOptionsSnapshot<Config> _snapshot;//在一个Scope中不变
        private readonly IOptions<Config> _snapshot1;//一直不变
        private readonly IOptionsMonitor<Config> _snapshot2;//配置变了，立即变
        private readonly IOptionsSnapshot<Proxy> _snapshotProxy;

        public TestController(IOptionsSnapshot<Config> snapshot, IOptionsSnapshot<Proxy> snapshotProxy, IOptions<Config> snapshot1, IOptionsMonitor<Config> snapshot2)
        {
            _snapshot = snapshot;
            _snapshotProxy = snapshotProxy;
            _snapshot1 = snapshot1;
            _snapshot2 = snapshot2;
        }
        public void Test()
        {
            Config config = _snapshot.Value;
            Console.WriteLine(_snapshot.Value.Age);
            Console.WriteLine(_snapshot.Value.Age);
            Console.WriteLine(_snapshotProxy.Value.Address);
        }
    }
}
