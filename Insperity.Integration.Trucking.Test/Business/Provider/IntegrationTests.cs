using System;
using System.Collections.Generic;
using System.Net.Http;
using Geotab.Checkmate;
using Insperity.Integration.Trucking.Business.Clients.KeepTruckin;
using Insperity.Integration.Trucking.Business.Events;
using Insperity.Integration.Trucking.Business.Events.Employee;
using Insperity.Integration.Trucking.Business.Events.Truck;
using Insperity.Integration.Trucking.Business.Model;
using Insperity.Integration.Trucking.Business.Providers.Geotab;
using Insperity.Integration.Trucking.Business.Providers.KeepTruckin;
using Insperity.Integration.Trucking.Core;
using Insperity.Integration.Trucking.Core.Configuration;
using Insperity.Integration.Trucking.Core.Configuration.Http;
using Insperity.Integration.Trucking.Test.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Insperity.Integration.Trucking.Test.Business.Provider
{
    [TestClass]
    public class IntegrationTests
    {
        private static readonly IDomainEvent DummyEvent = new EmployeeAddedEvent(new Employee(
           new Company(1, "Company",
               new List<EldProvider>() { new GeotabEldProvider("username", "password", "database") }), 1,
           "John", "Kesinger", "jkesinger"), DateTime.UtcNow);

        class FakeApi : API
        {
            public FakeApi() : this("username", "password", "", "")
            {
            }

            private FakeApi(string userName, string password, string sessionId, string database, string server = null,
                int timeout = 300000, HttpMessageHandler handler = null) : base(userName, password, sessionId, database,
                server, timeout, handler)
            {
            }
        }

        [TestMethod]
        public void ShouldNotHandleDifferentProviderAndHandledEvent()
        {
            //Arrange
            var keepTruckin = new KeepTruckin(new InsperityLogger(), new KeepTruckinEmployeeProvider(new KeepTruckinHttpWriteClient<Employee>(new FakeHttpClientFactory(new FakeHttpMessageHandler()), new EmployeeConfiguration(new KeepTruckinHttpConfiguration()))));

            //Act
            var handles = keepTruckin.Handles(DummyEvent);

            //Assert
            Assert.IsFalse(handles);
        }

        [TestMethod]
        public void ShouldHandleSameProviderAndHandledEvent()
        {
            //Arrange
            var geotab = new Trucking.Business.Providers.Geotab.Geotab(new InsperityLogger(), new GeotabEmployeeProvider(new FakeApi()));

            //Act
            var handles = geotab.Handles(DummyEvent);

            //Assert
            Assert.IsTrue(handles);
        }

        [TestMethod]
        public void ShouldNotHandleSameProviderAndNotHandledEvent()
        {
            //Arrange
            var geotab = new Trucking.Business.Providers.Geotab.Geotab(new InsperityLogger(), new GeotabEmployeeProvider(new FakeApi()));
            var truckEvent = new TruckAddedEvent(
                new Truck(1,
                    new Company(1, "Company", new List<EldProvider>() { new GeotabEldProvider("username", "password", "database") })),
                DateTime.UtcNow);

            //Act
            var handles = geotab.Handles(truckEvent);

            //Assert
            Assert.IsFalse(handles);
        }

        [TestMethod]
        public void ShouldNotHandleDifferentProviderAndNotHandledEvent()
        {
            //Arrange
            var geotab = new Trucking.Business.Providers.Geotab.Geotab(new InsperityLogger(), new GeotabEmployeeProvider(new FakeApi()));
            var truckEvent = new TruckAddedEvent(
                new Truck(1, new Company(1, "Company", new List<EldProvider>() { new KeepTruckinEldProvider("apiKey") })),
                DateTime.UtcNow);

            //Act
            var handles = geotab.Handles(truckEvent);

            //Assert
            Assert.IsFalse(handles);
        }
    }
}
