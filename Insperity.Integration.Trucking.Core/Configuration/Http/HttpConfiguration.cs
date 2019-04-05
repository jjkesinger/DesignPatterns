namespace Insperity.Integration.Trucking.Core.Configuration.Http
{
    public abstract class HttpConfiguration : Configuration
    {
        public abstract string BaseUrl { get; }
        public abstract string AddUrl { get; }
        public abstract string DeleteUrl { get; }
        public abstract string UpdateUrl { get; }
    }
}
