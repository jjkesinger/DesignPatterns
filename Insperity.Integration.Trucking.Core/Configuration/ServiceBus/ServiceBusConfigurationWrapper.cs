namespace Insperity.Integration.Trucking.Core.Configuration.ServiceBus
{
    //Decorator Pattern: allows for different configuration wrappers (Employee, Vehicle, etc. on top of "existing"
    //provider configuration classes)
    public abstract class ServiceBusConfigurationWrapper : ServiceBusConfiguration
    {
        public readonly ServiceBusConfiguration Configuration;
        protected ServiceBusConfigurationWrapper(ServiceBusConfiguration configuration)
        {
            Configuration = configuration;
        }

        public override string ConnectionString => Configuration.ConnectionString;
        public override string QueueName => Configuration.QueueName;
    }
}
