using MediatR;

namespace WebApiMediatR
{
    public class UserNameChangedHandler : NotificationHandler<UserNameChangeNotification>
    {
        protected override void Handle(UserNameChangeNotification notification)
        {
            Console.WriteLine("用户名从"+notification.oldUserName+"变成了"+notification.newUserName);
        }
    }
}
