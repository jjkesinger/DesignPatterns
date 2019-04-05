using Insperity.Integration.Trucking.Core.Utility;

namespace Insperity.Integration.Trucking.Business.Model
{
    public abstract class Entity : ISerializable
    {
        public int Id { get; set; }
    }
}
