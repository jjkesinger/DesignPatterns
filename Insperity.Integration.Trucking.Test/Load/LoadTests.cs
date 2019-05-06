using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Insperity.Integration.Trucking.Business.Events.Employee;
using Insperity.Integration.Trucking.Business.Events.Handling;
using Insperity.Integration.Trucking.Business.Events.Truck;
using Insperity.Integration.Trucking.Business.Model;
using Insperity.Integration.Trucking.Test.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Insperity.Integration.Trucking.Test.Load
{
    [TestClass]
    public class LoadTests
    {
        [TestMethod]
        public async Task ShouldLogAllHandledEventsAndNotLogEventsThatWereNotHandled()
        {
            var tasks = new ConcurrentBag<Task>();
            var sw = new Stopwatch();
            var container = Core.IoC.Container;
            TestIoC.InitBusiness(container);

            #region events
            var e = new EmployeeAddedEvent(
                new Employee(
                    new Company(2, "", new List<EldProvider>() { new KeepTruckinEldProvider("1234567890") }), 1,
                    "James", "Kesinger", "jakesinger"), DateTime.UtcNow);

            var f = new EmployeeDeletedEvent(
                new Employee(
                    new Company(2, "", new List<EldProvider>() { new GeotabEldProvider("jkes", "sdlkj", "company") }), 3,
                    "Justin", "Kesinger", "jukesinger"), DateTime.UtcNow);

            var g = new EmployeeUpdatedEvent(
                new Employee(
                    new Company(3, "", new List<EldProvider>() { new JjKellerEldProvider("654654654") }), 2,
                    "Jason", "Kesinger", "jkesinger"), DateTime.UtcNow);

            var h = new TruckAddedEvent(
                new Truck(1, new Company(3, "Unit 204", new List<EldProvider>() { new KeepTruckinEldProvider("654654654") })), DateTime.UtcNow);
            #endregion

            var publisher = Core.IoC.Container.Resolve<IEventPublisher>();

            sw.Start();
            Parallel.For(0, 2000, (i) =>
            {
                if (i % 2 == 0)
                {
                    tasks.Add(publisher.Publish(e));
                    
                }
                else if (i % 3 == 0)
                {
                    tasks.Add(publisher.Publish(g));
                    
                }
                else
                {
                    tasks.Add(publisher.Publish(f));
                }

                tasks.Add(publisher.Publish(h)); //no one listening to this event
            });

            await Task.WhenAll(tasks);
            sw.Stop();

            FakeLogger.Dictionary.TryGetValue(e, out var addCnt);
            FakeLogger.Dictionary.TryGetValue(g, out var updateCnt);
            FakeLogger.Dictionary.TryGetValue(f, out var deleteCnt);
            var hasHEvents = FakeLogger.Dictionary.TryGetValue(h, out var hCnt);

            Assert.AreEqual(1000, addCnt);
            Assert.AreEqual(333, updateCnt);
            Assert.AreEqual(667, deleteCnt);
            Assert.IsFalse(hasHEvents);
            Assert.AreEqual(0, hCnt);
            Assert.IsTrue(sw.Elapsed.TotalSeconds < 60);
        }
    }
}


