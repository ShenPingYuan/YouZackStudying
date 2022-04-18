// See https://aka.ms/new-console-template for more information
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

Console.WriteLine("Hello, World!");

var factory = new ConnectionFactory();
factory.HostName = "localhost";//RabbitMQ服务服务器地址
factory.DispatchConsumersAsync = true;
string exchangeName = "exchange1";//交换机名字
string eventName = "myEvent";//routingKey的值
using var conn = factory.CreateConnection();//tcp链接
using var channel = conn.CreateModel();
string queueName = "queue1";
channel.ExchangeDeclare(exchange: exchangeName, type: "direct");
channel.QueueDeclare(queue:queueName,durable:true,exclusive:false,autoDelete:false,arguments:null);
channel.QueueBind(queue: queueName, exchange: exchangeName, routingKey: eventName);

Console.WriteLine("链接成功");

var consumer = new AsyncEventingBasicConsumer(channel);
consumer.Received += Consumer_Received;
channel.BasicConsume(queue: queueName, autoAck: false, consumer: consumer);
Console.ReadLine();

async Task Consumer_Received(object sender, BasicDeliverEventArgs args)
{
    try
    {
        var bytes=args.Body.ToArray();
        string msg=Encoding.UTF8.GetString(bytes);
        Console.WriteLine(DateTime.Now + "收到了消息" + msg);
        //DeliveryTag:消息编号
        channel.BasicAck(args.DeliveryTag, multiple: false);//消息确认：Ack。对于没有确认的消息，可以进行消息的“重发”
        await Task.Delay(1000);
    }
    catch (Exception ex)
    {
        channel.BasicReject(args.DeliveryTag, true);
        Console.WriteLine("处理收到的消息出错"+ex);
    }
}