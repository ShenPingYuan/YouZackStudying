using Zack.EventBus;

namespace WebApiRabbitMQ2
{
    [EventName("UserCreated")]
    [EventName("OtherEventName")]
    public class EventHandlerA : IIntegrationEventHandler
    {
        public Task Handle(string eventName, string eventData)
        {
            Console.WriteLine("用户"+eventData+"被创建");
            return Task.CompletedTask;
        }
    }
}
