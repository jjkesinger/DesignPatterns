using System;
using System.Linq.Expressions;
using System.Net.Http;
using Insperity.Integration.Trucking.Business.Clients.KeepTruckin;
using Insperity.Integration.Trucking.Business.Model;
using Insperity.Integration.Trucking.Business.Providers.Geotab;
using Insperity.Integration.Trucking.Business.Providers.KeepTruckin;
using Insperity.Integration.Trucking.Core;
using Insperity.Integration.Trucking.Core.Configuration;
using Insperity.Integration.Trucking.Core.Configuration.Http;

namespace Insperity.Integration.Trucking.Business
{
    //Factory pattern

    public class BuilderFactory
    {
        
    }
    //Builder pattern
    public class IntegrationBuilder
    {
        private readonly Builder _builder;
        public IntegrationBuilder(Builder builder)
        {
            _builder = builder;
        }

        public void Build()
        {
            _builder?.BuildConfigurations();
            _builder?.BuildClients();
            _builder?.BuildProviders();
            _builder?.Build();
        }

        public Providers.Integration Get()
        {
            return _builder?.GetIntegration() ?? Providers.Integration.NULL;
        }
    }

    public class KeepTruckinBuilder : Builder
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private readonly ILogger _logger;

        private HttpConfiguration _employeeConfiguration;
        private KeepTruckinHttpWriteClient<Employee> _employeeClient;
        private KeepTruckinEmployeeProvider _employeeProvider;

        public KeepTruckinBuilder(ILogger logger, HttpClient httpClient, Func<dynamic> dataFunc)
        {
            _httpClient = httpClient;
            _apiKey = dataFunc();
            _logger = logger;
        }

        public override void BuildProviders()
        {
            _employeeProvider = new KeepTruckinEmployeeProvider(_employeeClient);
        }

        public override void BuildConfigurations()
        {
            var config = new KeepTruckinHttpConfiguration();
            _employeeConfiguration = new EmployeeConfiguration(config);
        }

        public override void BuildClients()
        {
            _employeeClient = new KeepTruckinHttpWriteClient<Employee>(_httpClient, _employeeConfiguration);
            _employeeClient.SetApiKey(_apiKey);
        }

        public override void Build()
        {
            Integration = new KeepTruckin(_logger, _employeeProvider);
        }
    }

    public class GeoTabBuilder : Builder
    {
        private readonly ILogger _logger;
        private GeotabEmployeeProvider _geotabProvider;
        private GeotabConfiguration _configuration;
        private readonly string _username;
        private readonly string _password;
        private readonly string _database;

        public GeoTabBuilder(ILogger logger, Func<dynamic> dataFunc)
        {
            _logger = logger;
            var data = dataFunc();
            _username = data.Username;
            _password = data.Password;
            _database = data.Database;
        }

        public override void BuildProviders()
        {
            _geotabProvider = new GeotabEmployeeProvider(_configuration);
        }

        public override void BuildConfigurations()
        {
            _configuration = new GeotabConfiguration(_username, _password, _database);
        }

        public override void BuildClients()
        { }

        public override void Build()
        {
            Integration = new Business.Providers.Geotab.Geotab(_logger, _geotabProvider);
        }
    }

    public abstract class Builder
    {
        protected Business.Providers.Integration Integration { get; set; }
        public abstract void BuildProviders();
        public abstract void BuildConfigurations();
        public abstract void BuildClients();
        public abstract void Build();
        public virtual Business.Providers.Integration GetIntegration()
        {
            return Integration;
        }
    }
}
