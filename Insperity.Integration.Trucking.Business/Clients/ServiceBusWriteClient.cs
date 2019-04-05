using System.Threading.Tasks;
using Insperity.Integration.Trucking.Business.Serialization;
using Insperity.Integration.Trucking.Core.Configuration.ServiceBus;
using Insperity.Integration.Trucking.Core.Utility;

namespace Insperity.Integration.Trucking.Business.Clients
{
    public abstract class ServiceBusWriteClient<T> : IWriteClient<T> where T: ISerializable
    {
        protected readonly ServiceBusConfiguration Configuration;
        protected readonly ClientSerializer<T> Serializer;

        protected ServiceBusWriteClient(ServiceBusConfiguration configuration,
            ClientSerializer<T> serializer)
        {
            Serializer = serializer ?? new JsonSerializer<T>();
            Configuration = configuration;
        }

        public abstract Task Add(T entity);
        public abstract Task Update(T entity);
        public abstract Task Delete(T entity);
    }
}
