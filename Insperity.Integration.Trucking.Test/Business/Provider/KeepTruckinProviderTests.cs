using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Insperity.Integration.Trucking.Business;
using Insperity.Integration.Trucking.Business.Clients;
using Insperity.Integration.Trucking.Business.Events;
using Insperity.Integration.Trucking.Business.Events.Employee;
using Insperity.Integration.Trucking.Business.Model;
using Insperity.Integration.Trucking.Business.Providers;
using Insperity.Integration.Trucking.Business.Providers.KeepTruckin;
using Insperity.Integration.Trucking.Core;
using Insperity.Integration.Trucking.Test.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Employee = Insperity.Integration.Trucking.Business.Model.Employee;

namespace Insperity.Integration.Trucking.Test.Business.Provider
{
    [TestClass]
    public class KeepTuckinProviderTests
    {
        private readonly Employee _employeeWithProviders = new Employee(
            new Company(1, "", new List<EldProvider>() { new KeepTruckinEldProvider("user") }), 1, "John", "Kesinger", "Jkesinger");

        [TestMethod]
        public void KeepTruckinStoreShouldHandleKeepTruckinProvider()
        {
            //Arrange
            var logger = new Mock<ILogger>();
            var httpClient = new FakeHttpClientFactory(new FakeHttpMessageHandler());

            //Act
            var store = new KeepTruckinStore(logger.Object, httpClient);

            //Assert
            Assert.AreEqual(IntegrationProvider.KeepTruckin, store.HandlesProvider);
        }

        [TestMethod]
        public void KeepTruckinStoreShouldCreateWithKeepTruckinEldProviderParameterObject()
        {
            //Arrange
            var logger = new Mock<ILogger>();
            var httpClient = new FakeHttpClientFactory(new FakeHttpMessageHandler());
            var store = new KeepTruckinStore(logger.Object, httpClient);
            var eld = new KeepTruckinEldProvider("api");

            //Act
            var integration = store.CreateIntegration(eld);

            //Assert
            Assert.IsNotNull(integration);
        }

        [TestMethod]
        public async Task KeepTruckinEmployeeProviderShouldCallApiLibraryOnAddEmployee()
        {
            //Arrange
            var api = new Mock<IWriteClient<Employee>>();
            var provider = new KeepTruckinEmployeeProvider(api.Object);

            //Act
            await provider.AddEmployee(_employeeWithProviders);

            //Assert
            api.Verify(f=> f.Add(_employeeWithProviders), Times.Once);
        }

        [TestMethod]
        public async Task KeepTruckinEmployeeProviderShouldCallApiLibraryOnUpdateEmployee()
        {
            //Arrange
            var api = new Mock<IWriteClient<Employee>>();
            var provider = new KeepTruckinEmployeeProvider(api.Object);

            //Act
            await provider.UpdateEmployee(_employeeWithProviders);

            //Assert
            api.Verify(f => f.Update(_employeeWithProviders), Times.Once);
        }

        [TestMethod]
        public async Task KeepTruckinEmployeeProviderShouldCallApiLibraryOnDeleteEmployee()
        {
            //Arrange
            var api = new Mock<IWriteClient<Employee>>();
            var provider = new KeepTruckinEmployeeProvider(api.Object);

            //Act
            await provider.DeleteEmployee(_employeeWithProviders);

            //Assert
            api.Verify(f => f.Delete(_employeeWithProviders), Times.Once);
        }

        [TestMethod]
        public async Task KeepTruckinShouldHandleEmployeeAddedEvent()
        {
            //Arrange
            var logger = new Mock<ILogger>();
            var employeeProvider = new Mock<IEmployeeWriteProvider>();
            var keepTruckin = new KeepTruckin(logger.Object, employeeProvider.Object);
            var handler = keepTruckin as IHandle<EmployeeAddedEvent>;
            var e = new EmployeeAddedEvent(_employeeWithProviders, DateTime.Now);

            //Act
            await handler.Handle(e);

            //Assert
            employeeProvider.Verify(f => f.AddEmployee(_employeeWithProviders), Times.Once);
            logger.Verify(f => f.LogMessage(e), Times.Once);
        }

        [TestMethod]
        public async Task KeepTruckinShouldHandleEmployeeUpdatedEvent()
        {
            //Arrange
            var logger = new Mock<ILogger>();
            var employeeProvider = new Mock<IEmployeeWriteProvider>();
            var keepTruckin = new KeepTruckin(logger.Object, employeeProvider.Object);
            var handler = keepTruckin as IHandle<EmployeeUpdatedEvent>;
            var e = new EmployeeUpdatedEvent(_employeeWithProviders, DateTime.Now);

            //Act
            await handler.Handle(e);

            //Assert
            employeeProvider.Verify(f => f.UpdateEmployee(_employeeWithProviders), Times.Once);
            logger.Verify(f => f.LogMessage(e), Times.Once);
        }

        [TestMethod]
        public async Task KeepTruckinShouldHandleEmployeeDeletedEvent()
        {
            //Arrange
            var logger = new Mock<ILogger>();
            var employeeProvider = new Mock<IEmployeeWriteProvider>();
            var keepTruckin = new KeepTruckin(logger.Object, employeeProvider.Object);
            var handler = keepTruckin as IHandle<EmployeeDeletedEvent>;
            var e = new EmployeeDeletedEvent(_employeeWithProviders, DateTime.Now);

            //Act
            await handler.Handle(e);

            //Assert
            employeeProvider.Verify(f => f.DeleteEmployee(_employeeWithProviders), Times.Once);
            logger.Verify(f => f.LogMessage(e), Times.Once);
        }
    }
}
