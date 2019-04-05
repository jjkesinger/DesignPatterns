using System;
using System.Collections.Generic;
using Insperity.Integration.Trucking.Business.Events.Employee;
using Insperity.Integration.Trucking.Business.Events.Truck;
using Insperity.Integration.Trucking.Business.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Insperity.Integration.Trucking.Test.Business.Events
{
    [TestClass]
    public class EventTests
    {
        private readonly Employee _employee = new Employee(
            new Company(1, ""), 1, "John", "Kesinger", "Jkesinger");

        private readonly Employee _employeeWithProviders = new Employee(
            new Company(1, "", new List<EldProvider>(){new KeepTruckinEldProvider("123")}), 1, "John", "Kesinger", "Jkesinger");

        private readonly Truck _truck = new Truck(1,
            new Company(1, "", new List<EldProvider>()));

        private readonly Truck _truckWithProviders = new Truck(1,
            new Company(1, "", new List<EldProvider>() {new KeepTruckinEldProvider("123")}));

        [TestMethod]
        public void EmployeeAddedEventShouldReturnEmptyListForCompanyWithNoIntegrationTypes()
        {
            //Act
            var eae = new EmployeeAddedEvent(_employee, DateTime.Now);

            //Assert
            Assert.IsNotNull(eae.IntegrationTypes);
            Assert.AreEqual(0, eae.IntegrationTypes.Count);
        }

        [TestMethod]
        public void EmployeeUpdatedEventShouldReturnEmptyListForCompanyWithNoIntegrationTypes()
        {
            //Act
            var eae = new EmployeeUpdatedEvent(_employee, DateTime.Now);

            //Assert
            Assert.IsNotNull(eae.IntegrationTypes);
            Assert.AreEqual(0, eae.IntegrationTypes.Count);
        }

        [TestMethod]
        public void EmployeeDeletedEventShouldReturnEmptyListForCompanyWithNoIntegrationTypes()
        {
            //Act
            var eae = new EmployeeDeletedEvent(_employee, DateTime.Now);

            //Assert
            Assert.IsNotNull(eae.IntegrationTypes);
            Assert.AreEqual(0, eae.IntegrationTypes.Count);
        }

        [TestMethod]
        public void EmployeeAddedEventShouldReturnListOfCompanyProviders()
        {
            //Act
            var eae = new EmployeeAddedEvent(_employeeWithProviders, DateTime.Now);

            //Assert
            Assert.IsNotNull(eae.IntegrationTypes);
            Assert.AreEqual(1, eae.IntegrationTypes.Count);
        }

        [TestMethod]
        public void EmployeeUpdatedEventShouldReturnListOfCompanyProviders()
        {
            //Act
            var eae = new EmployeeUpdatedEvent(_employeeWithProviders, DateTime.Now);

            //Assert
            Assert.IsNotNull(eae.IntegrationTypes);
            Assert.AreEqual(1, eae.IntegrationTypes.Count);
        }

        [TestMethod]
        public void EmployeeDeletedEventShouldReturnListOfCompanyProviders()
        {
            //Act
            var eae = new EmployeeDeletedEvent(_employeeWithProviders, DateTime.Now);

            //Assert
            Assert.IsNotNull(eae.IntegrationTypes);
            Assert.AreEqual(1, eae.IntegrationTypes.Count);
        }

        [TestMethod]
        public void TruckAddedEventShouldReturnEmptyListForCOmpanyWithNoProviders()
        {
            //Act
            var eae = new TruckAddedEvent(_truck, DateTime.Now);

            //Assert
            Assert.IsNotNull(eae.IntegrationTypes);
            Assert.AreEqual(0, eae.IntegrationTypes.Count);
        }

        [TestMethod]
        public void TruckAddedEventShouldReturnListOfCompanyProviders()
        {
            //Act
            var eae = new TruckAddedEvent(_truckWithProviders, DateTime.Now);

            //Assert
            Assert.IsNotNull(eae.IntegrationTypes);
            Assert.AreEqual(1, eae.IntegrationTypes.Count);
        }
    }
}
