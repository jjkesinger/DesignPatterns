using System.Threading.Tasks;

namespace Insperity.Integration.Trucking.Business.Events
{
    public interface IHandle<in T> where T : IDomainEvent
    {
        Task Handle(T domainEvent);
    }
}
