using System;
using System.Collections.Generic;
using Insperity.Integration.Trucking.Business.Model;

namespace Insperity.Integration.Trucking.Business.Events
{
    public interface IDomainEvent
    {
        List<EldProvider> IntegrationTypes { get; }
        DateTime EventDateTime { get; }
    }
}
