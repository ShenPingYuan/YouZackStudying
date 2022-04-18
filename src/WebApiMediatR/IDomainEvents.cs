using MediatR;

namespace WebApiMediatR
{
    public interface IDomainEvents
    {
        IEnumerable<INotification> GetDomainEvents();
        void AddDomainEvent(INotification notification);
        void ClearDomainEvents();
    }
}
