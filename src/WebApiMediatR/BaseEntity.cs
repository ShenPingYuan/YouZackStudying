using MediatR;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiMediatR
{
    public abstract class BaseEntity : IDomainEvents
    {
        [NotMapped]//==Ignore()
        private readonly IList<INotification> _events=new List<INotification>();

        public void AddDomainEvent(INotification notification)
        {
            _events.Add(notification);
        }

        public void ClearDomainEvents()
        {
            _events.Clear();
        }

        public IEnumerable<INotification> GetDomainEvents()
        {
            return _events;
        }
    }
}
