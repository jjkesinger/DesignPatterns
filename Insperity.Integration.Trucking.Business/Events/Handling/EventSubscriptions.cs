using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Insperity.Integration.Trucking.Business.Providers;

namespace Insperity.Integration.Trucking.Business.Events.Handling
{
    public class EventSubscriptions : ISubscriptionService
    {
        private readonly IEnumerable<IntegrationStore> _stores;
        public EventSubscriptions(IEnumerable<IntegrationStore> stores)
        {
            _stores = stores;
        }

        public async Task<IEnumerable<IHandle<T>>> GetSubscriptions<T>(IDomainEvent e) where T : IDomainEvent
        {
            var consumers = new ConcurrentBag<Providers.Integration>();
            Parallel.ForEach(e.IntegrationTypes, (integration, state) =>
            {
                IntegrationStore store;
                try
                {
                    //using the abstract factory pattern (as well as dependency injection), I'm able to implement this
                    //statement instead of having a switch statement to determine what provider it is and creating concrete
                    //implementations. 
                    store = _stores.SingleOrDefault(f => f.HandlesProvider == integration.IntegrationType);
                }
                catch (InvalidOperationException)
                {
                    throw new InvalidOperationException(
                        $"There are more than one IntegrationStores for integration {integration.IntegrationType}");
                }

                if (store == null)
                {
                    throw new NotImplementedException(
                        $"IntegrationStore for integration type {integration.IntegrationType} has not implemented.");
                }

                var eld = store.CreateIntegration(integration);
                if (eld.Handles(e))
                {
                    consumers.Add(eld);
                }
            });

            var result = consumers.Cast<IHandle<T>>();
            return await Task.FromResult(result);
        }
    }
}
