using System.Threading.Tasks;
using Insperity.Integration.Trucking.Business.Events;
using Insperity.Integration.Trucking.Business.Events.Employee;
using Insperity.Integration.Trucking.Core;

namespace Insperity.Integration.Trucking.Business.Providers.KeepTruckin
{
    //Bridge Pattern - separating abstraction (KeepTruckin) from implementation (IEmployeeProvider which in this case will be KeepTruckinProvider)
    public class KeepTruckin : Integration,  IHandle<EmployeeAddedEvent>, 
                                             IHandle<EmployeeUpdatedEvent>, 
                                             IHandle<EmployeeDeletedEvent>
    {
        private readonly IEmployeeWriteProvider _employeeProvider;
        public KeepTruckin(ILogger logger, IEmployeeWriteProvider employeeProvider) : base(logger, IntegrationProvider.KeepTruckin)
        {
            _employeeProvider = employeeProvider;
        }

        public async Task Handle(EmployeeAddedEvent domainEvent)
        {
            await LogMessage(domainEvent);
            await _employeeProvider.AddEmployee(domainEvent.Employee);
        }

        public async Task Handle(EmployeeUpdatedEvent domainEvent)
        {
            await LogMessage(domainEvent);
            await _employeeProvider.UpdateEmployee(domainEvent.Employee);
        }

        public async Task Handle(EmployeeDeletedEvent domainEvent)
        {
            await LogMessage(domainEvent);
            await _employeeProvider.DeleteEmployee(domainEvent.Employee);
        }
    }
}
