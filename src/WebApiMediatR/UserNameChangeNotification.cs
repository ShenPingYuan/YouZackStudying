using MediatR;

namespace WebApiMediatR
{
    public record UserNameChangeNotification(string oldUserName,string newUserName):INotification
    {
    }
}
