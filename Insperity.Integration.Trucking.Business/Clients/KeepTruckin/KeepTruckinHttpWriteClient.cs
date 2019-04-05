using System;
using Insperity.Integration.Trucking.Business.Serialization;
using Insperity.Integration.Trucking.Core.Configuration;
using Insperity.Integration.Trucking.Core.Configuration.Http;
using Insperity.Integration.Trucking.Core.Utility;

namespace Insperity.Integration.Trucking.Business.Clients.KeepTruckin
{
    public class KeepTruckinHttpWriteClient<T> : HttpWriteClient<T> where T : ISerializable
    {
        public KeepTruckinHttpWriteClient(IHttpClientFactory httpClientFactory, HttpConfiguration configuration, ClientSerializer<T> serializer = null) 
            : base(httpClientFactory, configuration, serializer)
        {
            var t = configuration as HttpConfigurationWrapper;
            if (t == null || t.Configuration.GetType() != typeof(KeepTruckinHttpConfiguration))
                throw new ArgumentException("Invalid configuration. Must use a valid KeepTruckin HTTP Configuration.", nameof(configuration));

            if (serializer != null && serializer.GetType() != typeof(JsonSerializer<T>))
                throw new ArgumentException("KeepTruckin uses json serialization.");
        }

        public void SetApiKey(string apiKey)
        {
            HttpClient.DefaultRequestHeaders.TryAddWithoutValidation("X-Key", apiKey);
        }
    }
}
