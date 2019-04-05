namespace Insperity.Integration.Trucking.Core.Configuration.ServiceBus
{
    public class EmployeeConfiguration : ServiceBusConfigurationWrapper
    {
        public EmployeeConfiguration(ServiceBusConfiguration configuration) : base(configuration)
        { }

        public override string QueueName => $"Employee_{Configuration.QueueName}";
    }
}
