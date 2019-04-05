using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Insperity.Integration.Trucking.Business;
using Insperity.Integration.Trucking.Business.Events;
using Insperity.Integration.Trucking.Business.Events.Employee;
using Insperity.Integration.Trucking.Business.Events.Handling;
using Insperity.Integration.Trucking.Business.Model;
using Insperity.Integration.Trucking.Business.Providers;
using Insperity.Integration.Trucking.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Insperity.Integration.Trucking.Test.Business.Events
{
    [TestClass]
    public class EventHandlingTests
    {
        public class TestEventHandler : Trucking.Business.Providers.Integration, IHandle<EmployeeAddedEvent>
        {
            public virtual async Task Handle(EmployeeAddedEvent domainEvent)
            {
                await Task.CompletedTask;
            }

            public TestEventHandler() : base(new InsperityLogger(), IntegrationProvider.KeepTruckin)
            {

            }
        }

        public class TestEventHanlderStore : IntegrationStore
        {
            public override Trucking.Business.Providers.Integration CreateIntegration(dynamic parameters)
            {
                return new TestEventHandler();
            }

            public override IntegrationProvider HandlesProvider => IntegrationProvider.KeepTruckin;
        }

        public class TestJjKellerEventHanlderStore : IntegrationStore
        {
            public override Trucking.Business.Providers.Integration CreateIntegration(dynamic parameters)
            {
                return new TestEventHandler();
            }

            public override IntegrationProvider HandlesProvider => IntegrationProvider.JjKeller;
        }

        public class TestEventHanlderDuplicateStore : IntegrationStore
        {
            public override Trucking.Business.Providers.Integration CreateIntegration(dynamic parameters)
            {
                return new TestEventHandler();
            }

            public override IntegrationProvider HandlesProvider => IntegrationProvider.KeepTruckin;
        }

        [TestMethod]
        public async Task EventPublisherShouldCallHandleOnEventHandlerWhenEventIsPublished()
        {
            //Arrange
            var logger = new Mock<ILogger>();
            var e = new EmployeeAddedEvent(
                new Employee(new Company(1, ""), 1, "", "", ""), DateTime.Now);
            var t = new List<Trucking.Business.Providers.Integration>();
            var kt = new Mock<TestEventHandler>();
            t.Add(kt.Object);
            var service = new Mock<ISubscriptionService>();
            service.Setup(f => f.GetSubscriptions<EmployeeAddedEvent>(e))
                .Returns(Task.FromResult(t.Cast<IHandle<EmployeeAddedEvent>>()));
            var publisher = new EventPublisher(logger.Object, service.Object);

            //Act
            await publisher.Publish(e);
            
            //Assert
            kt.Verify(f=>f.Handle(e), Times.Once);
        }

        [TestMethod]
        public async Task EventSubsriptionsShouldReturnListOfValidHandlers()
        {
            //Arrange
            var e = new EmployeeAddedEvent(
                new Employee(new Company(1, "", new List<EldProvider>() {new KeepTruckinEldProvider("sdlkfj")}), 1, "",
                    "", ""), DateTime.Now);
            
            var store = new TestEventHanlderStore();
            var service = new EventSubscriptions(new List<IntegrationStore>(){store});

            //Act
            var subscriptions = await service.GetSubscriptions<EmployeeAddedEvent>(e);

            //Assert
            Assert.AreEqual(1, subscriptions.Count());
        }

        [TestMethod]
        public async Task EventSubsriptionsShouldReturnEmptyListWhenEventProviderListDoesNotMatchProvidersReturnedFromSubscription()
        {
            //Arrange
            var e = new EmployeeAddedEvent(
                new Employee(new Company(1, "", new List<EldProvider>() { new JjKellerEldProvider("sdlkfj") }), 1, "",
                    "", ""), DateTime.Now);

            var store = new TestEventHanlderStore();
            var store2 = new TestJjKellerEventHanlderStore();
            var service = new EventSubscriptions(new List<IntegrationStore>() { store, store2 });

            //Act
            var subscriptions = await service.GetSubscriptions<EmployeeAddedEvent>(e);

            //Assert
            Assert.AreEqual(0, subscriptions.Count());
        }

        [TestMethod]
        [ExpectedException(typeof(AggregateException))]
        public async Task EventSubsriptionsShouldThrowErrorForEventThatDoesNotHaveAStore()
        {
            //Arrange
            var e = new EmployeeAddedEvent(
                new Employee(new Company(1, "", new List<EldProvider>() { new JjKellerEldProvider("sdlkfj") }), 1, "",
                    "", ""), DateTime.Now);

            var store = new TestEventHanlderStore();
            var service = new EventSubscriptions(new List<IntegrationStore>() { store });

            //Act
            try
            {
                await service.GetSubscriptions<EmployeeAddedEvent>(e);
            }
            catch (Exception exception)
            {
                //Assert
                Assert.AreEqual("IntegrationStore for integration type JjKeller has not implemented.", exception?.InnerException?.Message);
                throw;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(AggregateException))]
        public async Task EventSubsriptionsShouldThrowErrorForTwoStoreOfTheSameIntegrationProvider()
        {
            //Arrange
            var e = new EmployeeAddedEvent(
                new Employee(new Company(1, "", new List<EldProvider>() { new KeepTruckinEldProvider("sdlkfj") }), 1, "",
                    "", ""), DateTime.Now);

            var store = new TestEventHanlderStore();
            var store2 = new TestEventHanlderDuplicateStore();
            var service = new EventSubscriptions(new List<IntegrationStore>() { store, store2 });

            //Act
            try
            {
                await service.GetSubscriptions<EmployeeAddedEvent>(e);
            }
            catch (Exception exception)
            {
                //Assert
                Assert.AreEqual("There are more than one IntegrationStores for integration KeepTruckin", exception?.InnerException?.Message);
                throw;
            }
        }
    }
}
