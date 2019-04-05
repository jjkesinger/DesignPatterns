using System;
using Insperity.Integration.Trucking.Core.Configuration.Http;
using Insperity.Integration.Trucking.Core.Configuration.ServiceBus;

namespace Insperity.Integration.Trucking.Core.Configuration
{
    public class Configuration
    {
        private readonly ConfigurationHelper _configurationHelper;
        public Configuration() : this(new ConfigurationHelper())
        { }

        public Configuration(ConfigurationHelper configurationHelper)
        {
            _configurationHelper = configurationHelper;
        }

        public virtual string GetConfiguration(string key)
        {
            var configuration = _configurationHelper.GetConfiguration(key);
            if (configuration.HasValue)
            {
                return configuration.Value;
            }

            throw new InvalidOperationException($"Configuration key ({key}) not found in configuration settings.");
        }
    }

    public class KeepTruckinServiceBusConfiguration : ServiceBusConfiguration
    {
        public override string ConnectionString => GetConfiguration("KeepTruckinConnectionString");
        public override string QueueName => GetConfiguration("Queue");
    }

    public class KeepTruckinHttpConfiguration : HttpConfiguration
    {
        public override string BaseUrl => GetConfiguration("KeepTruckinBaseUrl");
        public override string AddUrl => GetConfiguration("KeepTruckinAddUrl");
        public override string DeleteUrl => GetConfiguration("KeepTruckinDeleteUrl");
        public override string UpdateUrl => GetConfiguration("KeepTruckinUpdateUrl");
    }

    public class JjKellerHttpConfiguration : HttpConfiguration
    {
        public override string BaseUrl => GetConfiguration("JJKellerBaseUrl");
        public override string AddUrl => GetConfiguration("JJKellerAddUrl");
        public override string DeleteUrl => GetConfiguration("JJKellerDeleteUrl");
        public override string UpdateUrl => GetConfiguration("JJKellerUpdateUrl");
    }

    public class GeotabConfiguration
    {
        public GeotabConfiguration(string username, string password, string database)
        {
            Username = username;
            Password = password;
            Database = database;
        }

        public string Username { get; }
        public string Password { get; }
        public string Database { get; }
    }
}
