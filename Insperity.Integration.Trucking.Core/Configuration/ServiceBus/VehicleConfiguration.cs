namespace Insperity.Integration.Trucking.Core.Configuration.ServiceBus
{
    public class VehicleConfiguration : ServiceBusConfigurationWrapper
    {
        public VehicleConfiguration(ServiceBusConfiguration configuration) : base(configuration)
        { }

        public override string QueueName => $"Vehicle_{Configuration.QueueName}";
    }
}
