using Insperity.Integration.Trucking.Business.Clients;
using Insperity.Integration.Trucking.Business.Clients.JJKeller;
using Insperity.Integration.Trucking.Business.Model;
using Insperity.Integration.Trucking.Core;
using Insperity.Integration.Trucking.Core.Configuration;
using Insperity.Integration.Trucking.Core.Configuration.Http;

namespace Insperity.Integration.Trucking.Business.Providers.JJKeller
{
    public class JjKellerStore : IntegrationStore
    {
        private readonly ILogger _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        public JjKellerStore(ILogger logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }

        public override Integration CreateIntegration(dynamic parameters)
        {
            var config = new JjKellerHttpConfiguration();
            var employeeClient = new JjKellerHttpWriteClient<Employee>(_httpClientFactory, new EmployeeConfiguration(config));
            employeeClient.SetApiKey(parameters.ApiKey);

            return new JjKeller(_logger, new JjKellerEmployeeProvider(employeeClient));
        }

        public override IntegrationProvider HandlesProvider => IntegrationProvider.JjKeller;
    }
}
