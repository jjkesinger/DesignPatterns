using System.Xml.Serialization;

namespace Insperity.Integration.Trucking.Business.Model
{
    //Only need to include the providers that will use XML serialization
    //For demo included all of them
    [XmlInclude(typeof(GeotabEldProvider))]
    [XmlInclude(typeof(KeepTruckinEldProvider))]
    [XmlInclude(typeof(JjKellerEldProvider))]
    public abstract class EldProvider : Entity
    {
        protected EldProvider(IntegrationProvider integrationType)
        {
            IntegrationType = integrationType;
            Name = integrationType.ToString();
        }

        public IntegrationProvider IntegrationType { get; }
        public string Name { get; }
    }
}
