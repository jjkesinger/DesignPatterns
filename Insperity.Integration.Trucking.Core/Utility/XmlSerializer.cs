using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Insperity.Integration.Trucking.Core.Utility
{
    public static class XmlSerializer<T> where T : ISerializable
    {
        public static string Serialize(T entity)
        {
            var xsSubmit = new XmlSerializer(typeof(T));
            using (var sww = new StringWriter())
            {
                using (var writer = XmlWriter.Create(sww))
                {
                    xsSubmit.Serialize(writer, entity);
                    return sww.ToString();
                }
            }
        }
    }
}
