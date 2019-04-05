using System;
using System.Collections.Generic;
using Insperity.Integration.Trucking.Business.Model;

namespace Insperity.Integration.Trucking.Business.Events.Truck
{
    public class TruckAddedEvent : IDomainEvent
    {
        public Model.Truck Truck { get; }
        public List<EldProvider> IntegrationTypes { get; }
        public DateTime EventDateTime { get; }

        public TruckAddedEvent(Model.Truck truck, DateTime eventDateTime)
        {
            Truck = truck;
            EventDateTime = eventDateTime;
            IntegrationTypes = truck?.Company?.Providers ?? new List<EldProvider>();
        }
    }
}
