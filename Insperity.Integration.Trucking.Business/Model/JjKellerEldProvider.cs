using System;

namespace Insperity.Integration.Trucking.Business.Model
{
    [Serializable]
    public class JjKellerEldProvider : EldProvider
    {
        public JjKellerEldProvider() : base(IntegrationProvider.JjKeller) { }
        public JjKellerEldProvider(string apiKey) : base(IntegrationProvider.JjKeller)
        {
            if (string.IsNullOrEmpty(apiKey))
                throw new ArgumentException("Invalid API Key");

            ApiKey = apiKey;
        }

        public string ApiKey { get; set; }
    }
}
