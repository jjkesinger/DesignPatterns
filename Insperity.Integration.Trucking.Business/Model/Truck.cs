namespace Insperity.Integration.Trucking.Business.Model
{
    public class Truck : Entity
    {
        public Truck() { }
        public Truck(int id, Company company)
        {
            Id = id;
            Company = company;
            CompanyId = company.Id;
        }

        public int CompanyId { get; set; }
        public Company Company { get; set; }
    }
}
