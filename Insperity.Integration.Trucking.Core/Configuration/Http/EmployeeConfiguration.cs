namespace Insperity.Integration.Trucking.Core.Configuration.Http
{
    public class EmployeeConfiguration : HttpConfigurationWrapper
    {
        public EmployeeConfiguration(HttpConfiguration configuration) : base(configuration)
        {
        }

        public override string AddUrl => $"{Configuration.AddUrl}_Employee";
        public override string UpdateUrl => $"{Configuration.UpdateUrl}_Employee";
        public override string DeleteUrl => $"{Configuration.DeleteUrl}_Employee";
    }
}
