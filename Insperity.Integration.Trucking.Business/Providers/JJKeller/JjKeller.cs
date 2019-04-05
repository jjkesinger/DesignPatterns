using System.Threading.Tasks;
using Insperity.Integration.Trucking.Business.Events;
using Insperity.Integration.Trucking.Business.Events.Employee;
using Insperity.Integration.Trucking.Core;

namespace Insperity.Integration.Trucking.Business.Providers.JJKeller
{
    //Bridge Pattern - separating abstraction (JJKeller) from implementation (IEmployeeProvider which in this case will be JJKellerProvider)
    public class JjKeller : Integration, IHandle<EmployeeAddedEvent>, 
                                           IHandle<EmployeeUpdatedEvent>
    {
        private readonly IEmployeeWriteProvider _employeeProvider;
        public JjKeller(ILogger logger, IEmployeeWriteProvider employeeProvider) : base(logger, IntegrationProvider.JjKeller)
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
    }
}
