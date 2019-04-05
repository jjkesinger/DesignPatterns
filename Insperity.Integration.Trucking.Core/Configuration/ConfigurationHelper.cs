using Insperity.Integration.Trucking.Core.Utility;

namespace Insperity.Integration.Trucking.Core.Configuration
{
    //Null object pattern
    public class ConfigurationHelper
    {
        public Maybe<string> GetConfiguration(string key)
        {
            if (key.Contains("BaseUrl")) //demo purposes only
                key = $"http://{key}.com";

            return Maybe<string>.Some(key); //would normally hook up to config manager in Infrastructure
        }
    }
}
