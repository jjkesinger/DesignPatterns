using System.Threading.Tasks;
using Insperity.Integration.Trucking.Core.Configuration.ServiceBus;
using Microsoft.Azure.ServiceBus;

namespace Insperity.Integration.Trucking.Business.ServiceBus.Azure
{
    public class AzureServiceBusMessageHandler : IServiceBusMessageHandler
    {
        private readonly QueueClient _queueClient;

        public AzureServiceBusMessageHandler(ServiceBusConfiguration configuration)
        {
            _queueClient = new QueueClient(configuration.ConnectionString, configuration.QueueName);
        }

        public async Task SendMessage(object message)
        {
            var msg = message as Message;
            await _queueClient.SendAsync(msg);
        }

        public async Task<object> CreateMessage(byte[] bytes)
        {
            var msg = new Message(bytes);
            return await Task.FromResult(msg);
        }
    }
}
