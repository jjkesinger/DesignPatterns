using System.Text;
using Insperity.Integration.Trucking.Core.Utility;

namespace Insperity.Integration.Trucking.Business.Serialization
{
    public class ApplicationXmlSerializer<T> : ClientSerializer<T> where T: ISerializable
    {
        public override string MediaType => "application/xml";
        public override Encoding Encoding => Encoding.UTF8;
        public override string Serialize(T entity)
        {
            return XmlSerializer<T>.Serialize(entity);
        }
    }
}
