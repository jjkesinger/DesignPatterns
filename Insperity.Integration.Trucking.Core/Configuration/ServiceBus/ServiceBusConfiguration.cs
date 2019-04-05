namespace Insperity.Integration.Trucking.Core.Configuration.ServiceBus
{
    public abstract class ServiceBusConfiguration : Configuration
    {
        public abstract string ConnectionString { get; }
        public abstract string QueueName { get; }
    }
}
