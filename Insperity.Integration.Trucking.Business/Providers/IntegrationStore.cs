namespace Insperity.Integration.Trucking.Business.Providers
{
    //Factory pattern
    public abstract class IntegrationStore
    {
        public abstract Integration CreateIntegration(dynamic parameters);
        public abstract IntegrationProvider HandlesProvider { get; }
    }
}
