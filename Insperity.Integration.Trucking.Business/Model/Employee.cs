using System;

namespace Insperity.Integration.Trucking.Business.Model
{
    [Serializable]
    public class Employee : Entity
    {
        public Employee() { }
        public Employee(Company company, int id, string firstName, string lastName, string username, string password = "", 
            DateTime? hiredate = null, DateTime? termDate = null)
        {
            FirstName = firstName;
            LastName = lastName;
            Username = username;
            Company = company;
            CompanyId = company.Id;
            Id = id;
            Password = password;
            HireDate = hiredate ?? DateTime.Now;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int CompanyId { get; set; }
        public Company Company { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public DateTime HireDate { get; set; }
        public DateTime? TerminationDate { get; set; }

        public void Terminate(DateTime terminationDate)
        {
            TerminationDate = terminationDate;
        }
    }
}
