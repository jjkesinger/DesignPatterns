namespace Insperity.Integration.Trucking.Core.Configuration.Http
{
    //Decorator Pattern: allows for different configuration wrappers (Employee, Vehicle, etc. on top of "existing"
    //provider configuration classes)
    public abstract class HttpConfigurationWrapper : HttpConfiguration
    {
        public readonly HttpConfiguration Configuration;
        protected HttpConfigurationWrapper(HttpConfiguration configuration)
        {
            Configuration = configuration;
        }

        public override string BaseUrl => Configuration.BaseUrl;
        public override string AddUrl => Configuration.AddUrl;
        public override string DeleteUrl => Configuration.DeleteUrl;
        public override string UpdateUrl => Configuration.UpdateUrl;
    }
}
