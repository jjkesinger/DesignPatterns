using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Geotab.Checkmate;
using Insperity.Integration.Trucking.Business;
using Insperity.Integration.Trucking.Business.Events;
using Insperity.Integration.Trucking.Business.Events.Employee;
using Insperity.Integration.Trucking.Business.Model;
using Insperity.Integration.Trucking.Business.Providers;
using Insperity.Integration.Trucking.Business.Providers.Geotab;
using Insperity.Integration.Trucking.Core;
using Insperity.Integration.Trucking.Test.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Employee = Insperity.Integration.Trucking.Business.Model.Employee;

namespace Insperity.Integration.Trucking.Test.Business.Provider
{
    [TestClass]
    public class GeotabProviderTests
    {
        private readonly Employee _employeeWithProviders = new Employee(
            new Company(1, "", new List<EldProvider>() { new GeotabEldProvider("user", "fs", "db") }), 1, "John", "Kesinger", "Jkesinger");

        public class FakeApi : API
        {
            public FakeApi() : this("username", "password", "", "", 
                null, 20000, new Mock<FakeHttpMessageHandler>().Object)
            {
                
            }

            public FakeApi(string userName, string password, string sessionId, string database, string server = null,
                int timeout = 300000, HttpMessageHandler handler = null) : base(userName, password, sessionId, database,
                server, timeout, handler)
            {
            }
        }

        [TestMethod]
        public void GeotabStoreShouldHandleGeotabProvider()
        {
            //Arrange
            var logger = new Mock<ILogger>();

            //Act
            var store = new GeotabStore(logger.Object);

            //Assert
            Assert.AreEqual(IntegrationProvider.Geotab, store.HandlesProvider);
        }

        [TestMethod]
        public void GeotabStoreShouldCreateWithGeotabEldProviderParameterObject()
        {
            //Arrange
            var logger = new Mock<ILogger>();
            var store = new GeotabStore(logger.Object);
            var eld = new GeotabEldProvider("username", "password", "database");

            //Act
            var integration = store.CreateIntegration(eld);

            //Assert
            Assert.IsNotNull(integration);
        }

        [TestMethod]
        [ExpectedException(typeof(NotImplementedException))]
        public async Task GeotabEmployeeProviderShouldCallApiLibraryOnAddEmployee()
        {
            //Arrange
            var api = new FakeApi("a", "a", "a","a", "a", 1000, new FakeHttpMessageHandler());
            var provider = new GeotabEmployeeProvider(api);

            //Act
            await provider.AddEmployee(_employeeWithProviders);

            //Assert
            //Since library does not have virtual member on Send call, cannot mock using Moq. Using fake http message handler and if Send gets
            //called a NotImplementedException will occur.
        }

        [TestMethod]
        [ExpectedException(typeof(NotImplementedException))]
        public async Task GeotabEmployeeProviderShouldCallApiLibraryOnUpdateEmployee()
        {
            //Arrange
            var api = new FakeApi("a", "a", "a", "a", "a", 1000, new FakeHttpMessageHandler());
            var provider = new GeotabEmployeeProvider(api);

            //Act
            await provider.UpdateEmployee(_employeeWithProviders);

            //Assert
            //Since library does not have virtual member on Send call, cannot mock using Moq. Using fake http message handler and if Send gets
            //called a NotImplementedException will occur.
        }

        [TestMethod]
        [ExpectedException(typeof(NotImplementedException))]
        public async Task GeotabEmployeeProviderShouldCallApiLibraryOnDeleteEmployee()
        {
            //Arrange
            var api = new FakeApi("a", "a", "a", "a", "a", 1000, new FakeHttpMessageHandler());
            var provider = new GeotabEmployeeProvider(api);

            //Act
            await provider.DeleteEmployee(_employeeWithProviders);

            //Assert
            //Since library does not have virtual member on Send call, cannot mock using Moq. Using fake http message handler and if Send gets
            //called a NotImplementedException will occur.
        }

        [TestMethod]
        public async Task GeotabShouldHandleEmployeeAddedEvent()
        {
            //Arrange
            var logger = new Mock<ILogger>();
            var employeeProvider = new Mock<IEmployeeWriteProvider>();
            var geotab = new Trucking.Business.Providers.Geotab.Geotab(logger.Object, employeeProvider.Object);
            var handler = geotab as IHandle<EmployeeAddedEvent>;
            var e = new EmployeeAddedEvent(_employeeWithProviders, DateTime.Now);

            //Act
            await handler.Handle(e);

            //Assert
            employeeProvider.Verify(f => f.AddEmployee(_employeeWithProviders), Times.Once);
            logger.Verify(f => f.LogMessage(e), Times.Once);
        }

        [TestMethod]
        public async Task GeotabShouldHandleEmployeeUpdatedEvent()
        {
            //Arrange
            var logger = new Mock<ILogger>();
            var employeeProvider = new Mock<IEmployeeWriteProvider>();
            var geotab = new Trucking.Business.Providers.Geotab.Geotab(logger.Object, employeeProvider.Object);
            var handler = geotab as IHandle<EmployeeUpdatedEvent>;
            var e = new EmployeeUpdatedEvent(_employeeWithProviders, DateTime.Now);

            //Act
            await handler.Handle(e);

            //Assert
            employeeProvider.Verify(f => f.UpdateEmployee(_employeeWithProviders), Times.Once);
            logger.Verify(f => f.LogMessage(e), Times.Once);
        }

        [TestMethod]
        public async Task GeotabShouldHandleEmployeeDeletedEvent()
        {
            //Arrange
            var logger = new Mock<ILogger>();
            var employeeProvider = new Mock<IEmployeeWriteProvider>();
            var geotab = new Trucking.Business.Providers.Geotab.Geotab(logger.Object, employeeProvider.Object);
            var handler = geotab as IHandle<EmployeeDeletedEvent>;
            var e = new EmployeeDeletedEvent(_employeeWithProviders, DateTime.Now);

            //Act
            await handler.Handle(e);

            //Assert
            employeeProvider.Verify(f => f.DeleteEmployee(_employeeWithProviders), Times.Once);
            logger.Verify(f => f.LogMessage(e), Times.Once);
        }
    }
}
