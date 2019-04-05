using System.Threading.Tasks;
using Insperity.Integration.Trucking.Core.Utility;

namespace Insperity.Integration.Trucking.Business.Clients
{
    public interface IWriteClient<in T> where T: ISerializable
    {
        Task Add(T entity);
        Task Update(T entity);
        Task Delete(T entity);
    }
}
