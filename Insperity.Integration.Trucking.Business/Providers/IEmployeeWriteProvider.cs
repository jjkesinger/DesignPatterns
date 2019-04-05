using System.Threading.Tasks;
using Insperity.Integration.Trucking.Business.Model;

namespace Insperity.Integration.Trucking.Business.Providers
{
    public interface IEmployeeWriteProvider
    {
        Task AddEmployee(Employee employee);
        Task UpdateEmployee(Employee employee);
        Task DeleteEmployee(Employee employee);
    }
}
