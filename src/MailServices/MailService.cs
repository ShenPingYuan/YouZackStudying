using ConfigServices;
using LogServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace MailServices
{
    public class MailService : IMailService
    {
        private readonly ILogProvider _log;
        private readonly IConfigService _configService;

        public MailService(IConfigService configService, ILogProvider log)
        {
            _configService = configService;
            _log = log;
        }

        public void Send(string title, string to, string body)
        {
            _log.LogInfo("准备发送邮件");
            string smtpServer = _configService.GetValue("SmtpServer");
            string userName=_configService.GetValue("username");
            string password=_configService.GetValue("password");
            Console.WriteLine("发送邮件");
            _log.LogInfo("邮件发送完成");
        }
    }
}
