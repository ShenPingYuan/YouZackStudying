// See https://aka.ms/new-console-template for more information
using RabbitMQ.Client;
using System.Text;

Console.WriteLine("Hello, World!");

var factory = new ConnectionFactory();
factory.HostName = "localhost";//RabbitMQ服务服务器地址
factory.DispatchConsumersAsync = true;
string exchangeName = "exchange1";//交换机名字
string eventName = "myEvent";//routingKey的值
using var conn=factory.CreateConnection();//tcp链接
Console.WriteLine("链接成功");
while (true)
{
    Console.WriteLine("请输入消息：");
    string? msg = Console.ReadLine();//待发送的消息
    if (msg == null) msg = "No Message";
    using(var channel=conn.CreateModel())//创建信道，虚拟信道
    {
        var properties=channel.CreateBasicProperties();
        properties.DeliveryMode = 2;
        channel.ExchangeDeclare(exchangeName, type: "direct");//声明交换机
        byte[] body=Encoding.UTF8.GetBytes(msg);
        channel.BasicPublish(exchange:exchangeName,routingKey:eventName,mandatory:true,basicProperties:properties,body:body);
    }
    Console.WriteLine("发布了消息"+msg);
    Thread.Sleep(1000);
}