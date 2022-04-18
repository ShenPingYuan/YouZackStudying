using MediatR;

namespace WebApiMediatR
{
    public record NewUserNotification(string UserName,DateTime Time):INotification
    {
    }
}
