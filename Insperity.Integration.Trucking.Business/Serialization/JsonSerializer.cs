using System.Text;
using Insperity.Integration.Trucking.Core.Utility;
using Newtonsoft.Json;

namespace Insperity.Integration.Trucking.Business.Serialization
{
    public class JsonSerializer<T> : ClientSerializer<T> where T: ISerializable
    {
        public override string MediaType => "application/json";
        public override Encoding Encoding => Encoding.UTF8;
        public override string Serialize(T entity)
        {
            return JsonConvert.SerializeObject(entity);
        }
    }
}
