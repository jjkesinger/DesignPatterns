using System.Text;
using Insperity.Integration.Trucking.Core.Utility;

namespace Insperity.Integration.Trucking.Business.Serialization
{
    public abstract class ClientSerializer<T> where T : ISerializable
    {
        public abstract string MediaType{ get; }
        public abstract Encoding Encoding { get; }
        public abstract string Serialize(T entity);
    }
}
