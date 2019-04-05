using System.Text;
using Insperity.Integration.Trucking.Core.Utility;

namespace Insperity.Integration.Trucking.Business.Serialization
{
    public class TextXmlSerializer<T> : ClientSerializer<T> where T: ISerializable
    {
        public override string MediaType => "text/xml";
        public override Encoding Encoding => Encoding.ASCII;
        public override string Serialize(T entity)
        {
            return XmlSerializer<T>.Serialize(entity);
        }
    }
}
