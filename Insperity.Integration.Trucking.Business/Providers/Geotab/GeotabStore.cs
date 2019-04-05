using Insperity.Integration.Trucking.Core;
using Insperity.Integration.Trucking.Core.Configuration;

namespace Insperity.Integration.Trucking.Business.Providers.Geotab
{
    public class GeotabStore : IntegrationStore
    {
        private readonly ILogger _logger;
        public GeotabStore(ILogger logger)
        {
            _logger = logger;
        }

        public override Integration CreateIntegration(dynamic parameters)
        {
            return new Geotab(_logger,
                new GeotabEmployeeProvider(new GeotabConfiguration(parameters.Username, parameters.Password,
                    parameters.Database)));
        }

        public override IntegrationProvider HandlesProvider => IntegrationProvider.Geotab;
    }
}
