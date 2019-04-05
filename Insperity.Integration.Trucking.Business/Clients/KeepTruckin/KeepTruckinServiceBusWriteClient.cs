using System;
using Insperity.Integration.Trucking.Business.Serialization;
using Insperity.Integration.Trucking.Business.ServiceBus;
using Insperity.Integration.Trucking.Core.Configuration;
using Insperity.Integration.Trucking.Core.Configuration.ServiceBus;
using Insperity.Integration.Trucking.Core.Utility;

namespace Insperity.Integration.Trucking.Business.Clients.KeepTruckin
{
    //This class is to demonstrate the ability to change client out should KeepTruckin switch from an Http api to 
    //a service bus api. Using the bridge pattern, we can swap out WriteClients in the KeepTruckinEmployeeProvider class
    public class KeepTruckinServiceBusWriteClient<T> : ServiceBusWriteClient<T> where T: ISerializable
    {
        public KeepTruckinServiceBusWriteClient(ServiceBusConfiguration configuration, ClientSerializer<T> serializer,
            IServiceBusMessageHandler serviceBusMessageHandler) : base(configuration, serializer, serviceBusMessageHandler)
        {
            var t = configuration as ServiceBusConfigurationWrapper;
            if (t == null || t.Configuration.GetType() != typeof(KeepTruckinServiceBusConfiguration))
                throw new ArgumentException("Invalid configuration. Must use a valid KeepTruckin Service Bus Configuration.", nameof(configuration));

            if (serializer.GetType() != typeof(ApplicationXmlSerializer<T>))
                throw new ArgumentException("KeepTruckin uses Application XML serialization.");
        }
    }
}
