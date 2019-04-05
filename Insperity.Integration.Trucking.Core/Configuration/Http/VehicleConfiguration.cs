namespace Insperity.Integration.Trucking.Core.Configuration.Http
{
    public class VehicleConfiguration : HttpConfigurationWrapper
    {
        public VehicleConfiguration(HttpConfiguration configuration) : base(configuration)
        {
        }

        public override string AddUrl => $"{Configuration.AddUrl}_Vehicle";
        public override string UpdateUrl => $"{Configuration.UpdateUrl}_Vehicle";
        public override string DeleteUrl => $"{Configuration.DeleteUrl}_Vehicle";
    }
}
