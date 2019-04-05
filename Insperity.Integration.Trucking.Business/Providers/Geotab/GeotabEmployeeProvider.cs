using System;
using System.Threading;
using System.Threading.Tasks;
using Geotab.Checkmate.ObjectModel;
using Insperity.Integration.Trucking.Core.Configuration;
using Employee = Insperity.Integration.Trucking.Business.Model.Employee;
using GeotabIntegration = Geotab.Checkmate;

namespace Insperity.Integration.Trucking.Business.Providers.Geotab
{
    //Adapter pattern that adapt IEmployeeWriteProvider to the Geotab 3rd party library
    public class GeotabEmployeeProvider : IEmployeeWriteProvider
    {
        private readonly GeotabIntegration.API _api;
        public GeotabEmployeeProvider(GeotabIntegration.API api)
        {
            _api = api;
            //_api.Authenticate();
        }

        public GeotabEmployeeProvider(GeotabConfiguration config) 
            : this(new GeotabIntegration.API(config.Username, config.Password, string.Empty, config.Database))
        { }

        public async Task AddEmployee(Employee employee)
        {
            var user = CreateBasicUser(employee);
#if !DEBUG
            await _api.CallAsync<Id>("Add", typeof(User),new { entity = user });
#else
            Thread.Sleep(300);
            await Task.CompletedTask;
#endif
        }

        public async Task UpdateEmployee(Employee employee)
        {
            var user = CreateBasicUser(employee);
#if !DEBUG
            await _api.CallAsync<Id>("Update", typeof(User), new { entity = user });
#else
            Thread.Sleep(300);
            await Task.CompletedTask;
#endif
        }

        public async Task DeleteEmployee(Employee employee)
        {
            await UpdateEmployee(employee); //Cannot delete in Geotab: only deactivate
        }

        private User CreateBasicUser(Employee employee)
        {
            var maxValue = employee.TerminationDate ?? System.TimeZoneInfo.ConvertTimeToUtc(DateTime.MaxValue);
            var minValue = DateTime.MinValue;
            var password = employee.Password;

            return User.CreateBasicUser(null, employee.Username, employee.FirstName, employee.LastName, password, null,
                null, null, minValue, maxValue, null, null, 
                null, null);
        }
    }
}
