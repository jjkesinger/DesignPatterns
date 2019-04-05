using System.Threading.Tasks;
using Insperity.Integration.Trucking.Business.Serialization;
using Insperity.Integration.Trucking.Business.ServiceBus;
using Insperity.Integration.Trucking.Core.Configuration.ServiceBus;
using Insperity.Integration.Trucking.Core.Utility;

namespace Insperity.Integration.Trucking.Business.Clients
{
    public abstract class ServiceBusWriteClient<T> : IWriteClient<T> where T: ISerializable
    {
        protected readonly ServiceBusConfiguration Configuration;
        protected readonly ClientSerializer<T> Serializer;
        protected readonly IServiceBusMessageHandler MessageHandler;

        protected ServiceBusWriteClient(ServiceBusConfiguration configuration,
            ClientSerializer<T> serializer, IServiceBusMessageHandler messageHandler)
        {
            Serializer = serializer ?? new JsonSerializer<T>();
            Configuration = configuration;
            MessageHandler = messageHandler;
        }

        public virtual async Task Add(T entity)
        {
            await SendMessage(entity);
        }

        public virtual async Task Update(T entity)
        {
            await SendMessage(entity);
        }

        public virtual async Task Delete(T entity)
        {
            await SendMessage(entity);
        }

        private async Task SendMessage(T entity)
        {
            var message = await CreateMessage(entity);
            await MessageHandler.SendMessage(message);
        }

        private async Task<object> CreateMessage(T entity)
        {
            var serializedMessage = Serializer.Serialize(entity);
            var bytes = Serializer.Encoding.GetBytes(serializedMessage);
            return await MessageHandler.CreateMessage(bytes);
        }
    }
}
