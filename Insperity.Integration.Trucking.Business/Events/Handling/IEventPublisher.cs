using System.Threading.Tasks;

namespace Insperity.Integration.Trucking.Business.Events.Handling
{
    public interface IEventPublisher
    {
        Task Publish<T>(T eventMessage) where T : IDomainEvent;
    }
}
