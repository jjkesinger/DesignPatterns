using System;

namespace Insperity.Integration.Trucking.Business.Model
{
    [Serializable]
    public class KeepTruckinEldProvider : EldProvider
    {
        public KeepTruckinEldProvider() : base(IntegrationProvider.KeepTruckin) { }
        public KeepTruckinEldProvider(string apiKey) : base(IntegrationProvider.KeepTruckin)
        {
            if (string.IsNullOrEmpty(apiKey))
                throw new ArgumentException("Invalid API Key");

            ApiKey = apiKey;
        }

        public string ApiKey { get; set; }
    }
}
