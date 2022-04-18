using MediatR;

namespace WebApiMediatR
{
    public record PostNotification(string body):INotification;
}
