using System;
using Insperity.Integration.Trucking.Business.Serialization;
using Insperity.Integration.Trucking.Core.Configuration;
using Insperity.Integration.Trucking.Core.Configuration.Http;
using Insperity.Integration.Trucking.Core.Utility;

namespace Insperity.Integration.Trucking.Business.Clients.JJKeller
{
    public class JjKellerHttpWriteClient<T> : HttpWriteClient<T> where T : ISerializable
    {
        public JjKellerHttpWriteClient(IHttpClientFactory httpClientFactory, HttpConfiguration configuration, ClientSerializer<T> serializer = null) 
            : base(httpClientFactory, configuration, serializer)
        {
            var t = configuration as HttpConfigurationWrapper;
            if (t == null || t.Configuration.GetType() != typeof(JjKellerHttpConfiguration))
                throw new ArgumentException("Invalid configuration. Must use a valid JJ Keller HTTP Configuration.", nameof(configuration));
        }

        public void SetApiKey(string apiKey)
        {
            HttpClient.DefaultRequestHeaders.Add("Keller-API-Key", apiKey);
        }
    }
}
