using System.Collections.Generic;
using System.Threading.Tasks;

namespace Insperity.Integration.Trucking.Business.Events.Handling
{
    public interface ISubscriptionService
    {
        Task<IEnumerable<IHandle<T>>> GetSubscriptions<T>(IDomainEvent e) where T : IDomainEvent;
    }
}
