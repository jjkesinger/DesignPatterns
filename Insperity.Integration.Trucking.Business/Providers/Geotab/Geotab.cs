using System.Threading.Tasks;
using Insperity.Integration.Trucking.Business.Events;
using Insperity.Integration.Trucking.Business.Events.Employee;
using Insperity.Integration.Trucking.Core;

namespace Insperity.Integration.Trucking.Business.Providers.Geotab
{
    //Bridge Pattern - separating abstraction (Geotab) from implementation (IEmployeeProvider which in this case will be KeepTruckinProvider)
    public class Geotab : Integration, IHandle<EmployeeAddedEvent>,
                                         IHandle<EmployeeUpdatedEvent>,
                                         IHandle<EmployeeDeletedEvent>

    {
        private readonly IEmployeeWriteProvider _employeeProvider;
        private readonly ILogger _logger;
        public Geotab(ILogger logger, IEmployeeWriteProvider employeeProvider) : base(logger, IntegrationProvider.Geotab)
        {
            _employeeProvider = employeeProvider;
            _logger = logger;
        }

        public async Task Handle(EmployeeAddedEvent domainEvent)
        {
            await _logger.LogMessage(domainEvent);
            await _employeeProvider.AddEmployee(domainEvent.Employee);
        }

        public async Task Handle(EmployeeUpdatedEvent domainEvent)
        {
            await _logger.LogMessage(domainEvent);
            await _employeeProvider.UpdateEmployee(domainEvent.Employee);
        }

        public async Task Handle(EmployeeDeletedEvent domainEvent)
        {
            await _logger.LogMessage(domainEvent);
            await _employeeProvider.DeleteEmployee(domainEvent.Employee);
        }
    }
}
