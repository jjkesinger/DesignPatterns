using System;
using System.Text;
using System.Threading.Tasks;
using Insperity.Integration.Trucking.Business.Serialization;
using Insperity.Integration.Trucking.Core.Configuration;
using Insperity.Integration.Trucking.Core.Configuration.ServiceBus;
using Insperity.Integration.Trucking.Core.Utility;
using Microsoft.Azure.ServiceBus;

namespace Insperity.Integration.Trucking.Business.Clients.KeepTruckin
{
    //This class is to demonstrate the ability to change client out should KeepTruckin switch from an Http api to 
    //a service bus api. Using the bridge pattern, we can swap out WriteClients in the KeepTruckinEmployeeProvider class
    public class KeepTruckinServiceBusWriteClient<T> : ServiceBusWriteClient<T> where T: ISerializable
    {
        private readonly QueueClient _queueClient;
        public KeepTruckinServiceBusWriteClient(ServiceBusConfiguration configuration, ClientSerializer<T> serializer) : base(configuration, serializer)
        {
            var t = configuration as ServiceBusConfigurationWrapper;
            if (t == null || t.Configuration.GetType() != typeof(KeepTruckinServiceBusConfiguration))
                throw new ArgumentException("Invalid configuration. Must use a valid KeepTruckin Service Bus Configuration.", nameof(configuration));

            if (serializer.GetType() != typeof(JsonSerializer<T>))
                throw new ArgumentException("KeepTruckin uses json serialization.");

            _queueClient = new QueueClient(configuration.ConnectionString, configuration.QueueName);
        }

        public override async Task Add(T entity)
        {
            await SendMessage(entity);
        }

        public override async Task Update(T entity)
        {
            await SendMessage(entity);
        }

        public override async Task Delete(T entity)
        {
            await SendMessage(entity);
        }

        private async Task SendMessage(T entity)
        {
            var message = CreateMessage(entity);
            await _queueClient.SendAsync(message);
        }

        private Message CreateMessage(T entity)
        {
            var serializedMessage = Serializer.Serialize(entity);
            var bytes = Encoding.UTF8.GetBytes(serializedMessage);
            return new Message(bytes);
        }
    }
}
