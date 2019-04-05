using System;
using System.Threading.Tasks;
using Insperity.Integration.Trucking.Business.Clients;
using Insperity.Integration.Trucking.Business.Model;

namespace Insperity.Integration.Trucking.Business.Providers.JJKeller
{
    public class JjKellerEmployeeProvider : IEmployeeWriteProvider
    {
        private readonly IWriteClient<Employee> _employeeClient;
        public JjKellerEmployeeProvider(IWriteClient<Employee> employeeClient)
        {
            _employeeClient = employeeClient;
        }

        public async Task AddEmployee(Employee employee)
        {
            await _employeeClient.Add(employee);
        }

        public async Task UpdateEmployee(Employee employee)
        {
            await _employeeClient.Update(employee);
        }

        public async Task DeleteEmployee(Employee employee)
        {
            await Task.CompletedTask;
            throw new NotSupportedException("JJ Keller does not support deleting employees.");
        }
    }
}
