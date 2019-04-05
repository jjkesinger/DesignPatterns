using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using Insperity.Integration.Trucking.Core;

namespace Insperity.Integration.Trucking.Business.Events.Handling
{
    public class EventPublisher : IEventPublisher
    {
        private readonly ISubscriptionService _subscriptionService;
        private readonly ILogger _logger;
        public EventPublisher(ILogger logger, ISubscriptionService subscriptionService)
        {
            _subscriptionService = subscriptionService;
            _logger = logger;
        }

        public async Task Publish<T>(T eventMessage) where T : IDomainEvent
        {
            var tasks = new ConcurrentBag<Task>();

            var subscriptions = await _subscriptionService.GetSubscriptions<T>(eventMessage);
            Parallel.ForEach(subscriptions, (handler) =>
            {
                var task = PublishToConsumer(handler, eventMessage);
                tasks.Add(task);
            });

            await Task.WhenAll(tasks);
        }

        private async Task PublishToConsumer<T>(IHandle<T> handler, T eventMessage) where T : IDomainEvent
        {
            try
            {
                await handler.Handle(eventMessage);
            }
            catch (Exception e)
            {
                await _logger.LogError(e);
            }
        }
    }
}
