using System.Linq;
using System.Threading.Tasks;
using Insperity.Integration.Trucking.Business.Events;
using Insperity.Integration.Trucking.Core;

namespace Insperity.Integration.Trucking.Business.Providers
{
    public abstract class Integration
    {
        public IntegrationProvider IntegrationType { get; }
        private readonly ILogger _logger;
        protected Integration(ILogger logger, IntegrationProvider integrationType)
        {
            _logger = logger;
            IntegrationType = integrationType;
        }

        protected async Task LogMessage(object message)
        {
            await _logger.LogMessage(message);
        }

        public bool Handles(IDomainEvent domainEvent)
        {
            var handlesEventIntegrationType = domainEvent.IntegrationTypes.Any(f=>f.IntegrationType == IntegrationType);
            if (handlesEventIntegrationType == false)
                return false;

            var handlesInterface = GetType().GetInterfaces()
                .Any(x => x.IsGenericType
                       && x.GetGenericTypeDefinition() == typeof(IHandle<>)
                       && x.GenericTypeArguments[0] == domainEvent.GetType());

            return handlesInterface;
        }
    }
}
