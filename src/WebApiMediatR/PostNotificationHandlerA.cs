using MediatR;

namespace WebApiMediatR
{
    public class PostNotificationHandlerA : NotificationHandler<PostNotification>
    {
        protected override void Handle(PostNotification notification)
        {
            Console.WriteLine("处理："+notification.body);
        }
    }
}
