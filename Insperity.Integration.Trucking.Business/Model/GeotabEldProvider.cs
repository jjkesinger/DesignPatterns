using System;

namespace Insperity.Integration.Trucking.Business.Model
{
    [Serializable]
    public class GeotabEldProvider : EldProvider
    {
        public GeotabEldProvider() : base(IntegrationProvider.Geotab) { }
        public GeotabEldProvider(string username, string password, string database) : base(IntegrationProvider.Geotab)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                throw new ArgumentException("Invalid username or password.");

            if (string.IsNullOrEmpty(database))
                throw new ArgumentException("Database is required.");

            Username = username;
            Password = password;
            Database = database;
        }

        public string Username { get; set; }
        public string Password { get; set; }
        public string Database { get; set; }
    }
}
