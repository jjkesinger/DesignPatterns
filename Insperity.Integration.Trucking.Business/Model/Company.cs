using System;
using System.Collections.Generic;
using System.Linq;

namespace Insperity.Integration.Trucking.Business.Model
{
    [Serializable]
    public class Company : Entity
    {
        public Company() { }
        public Company(int id, string name, List<EldProvider> eldProviders = null)
        {
            Name = name;
            Id = id;
            Providers = new List<EldProvider>();

            if (eldProviders?.Any(f => f.IntegrationType != IntegrationProvider.None) == true)
            {
                Providers = eldProviders;
            }
        }

        public string Name { get; set; }
        public List<EldProvider> Providers { get; set; }
    }
}
