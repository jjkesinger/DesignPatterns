using System.Threading.Tasks;
using Insperity.Integration.Trucking.Business.Clients;
using Insperity.Integration.Trucking.Business.Model;

namespace Insperity.Integration.Trucking.Business.Providers.KeepTruckin
{
    public class KeepTruckinEmployeeProvider : IEmployeeWriteProvider
    {
        private readonly IWriteClient<Employee> _employeeClient;
        public KeepTruckinEmployeeProvider(IWriteClient<Employee> employeeClient)
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
            await _employeeClient.Delete(employee);
        }
    }
}
