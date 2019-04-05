using Insperity.Integration.Trucking.Business.Clients;
using Insperity.Integration.Trucking.Business.Clients.KeepTruckin;
using Insperity.Integration.Trucking.Business.Model;
using Insperity.Integration.Trucking.Core;
using Insperity.Integration.Trucking.Core.Configuration;
using Insperity.Integration.Trucking.Core.Configuration.Http;

namespace Insperity.Integration.Trucking.Business.Providers.KeepTruckin
{
    public class KeepTruckinStore : IntegrationStore
    {
        private readonly ILogger _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        public KeepTruckinStore(ILogger logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }

        public override Integration CreateIntegration(dynamic parameters)
        {
            var config = new KeepTruckinHttpConfiguration();
            var employeeClient = new KeepTruckinHttpWriteClient<Employee>(_httpClientFactory, new EmployeeConfiguration(config));
            employeeClient.SetApiKey(parameters.ApiKey);

            return new KeepTruckin(_logger, new KeepTruckinEmployeeProvider(employeeClient));
        }

        public override IntegrationProvider HandlesProvider => IntegrationProvider.KeepTruckin;
    }
}
