using System;
using System.Collections.Generic;
using Insperity.Integration.Trucking.Business.Model;

namespace Insperity.Integration.Trucking.Business.Events.Employee
{
    public class EmployeeUpdatedEvent : IDomainEvent
    {
        public Model.Employee Employee { get; }
        public List<EldProvider> IntegrationTypes { get; }
        public DateTime EventDateTime { get; }
        public EmployeeUpdatedEvent(Model.Employee employee, DateTime eventDateTime)
        {
            Employee = employee;
            EventDateTime = eventDateTime;
            IntegrationTypes = employee?.Company?.Providers ?? new List<EldProvider>();
        }
    }
}
