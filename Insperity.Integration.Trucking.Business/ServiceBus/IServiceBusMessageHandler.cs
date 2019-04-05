using System.Threading.Tasks;

namespace Insperity.Integration.Trucking.Business.ServiceBus
{
    public interface IServiceBusMessageHandler
    {
        Task SendMessage(object message);
        Task<object> CreateMessage(byte[] bytes);
    }
}
