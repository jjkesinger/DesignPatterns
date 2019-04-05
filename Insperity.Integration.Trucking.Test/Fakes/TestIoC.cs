using System.Net.Http;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.Windsor;
using Insperity.Integration.Trucking.Business.Clients;
using Insperity.Integration.Trucking.Business.Events;
using Insperity.Integration.Trucking.Business.Events.Handling;
using Insperity.Integration.Trucking.Business.Providers;
using Insperity.Integration.Trucking.Core;
using Insperity.Integration.Trucking.Test.Fakes;

namespace Insperity.Integration.Trucking.Test
{
    public static class TestIoC
    {
        public static void InitBusiness(IWindsorContainer container)
        {
            container.Kernel.Resolver.AddSubResolver(new CollectionResolver(container.Kernel, true));
            container.Register(
                Component.For<ILogger>().ImplementedBy<FakeLogger>().LifeStyle.Singleton,
                Component.For<HttpMessageHandler>().ImplementedBy<FakeHttpMessageHandler>().LifeStyle.Transient,
                Component.For<IHttpClientFactory>().ImplementedBy<FakeHttpClientFactory>().LifeStyle.Transient,
                Component.For<ISubscriptionService>().ImplementedBy<EventSubscriptions>().LifeStyle.Transient,
                Component.For<IEventPublisher>().ImplementedBy<EventPublisher>().LifeStyle.Transient,
                Classes.FromAssemblyContaining(typeof(IntegrationStore)).BasedOn(typeof(IntegrationStore)).WithServiceFromInterface().LifestyleTransient(),
                Classes.FromAssemblyContaining(typeof(IntegrationStore)).BasedOn(typeof(IHandle<>)).WithServiceAllInterfaces().LifestyleTransient()
            );
        }
    }
}
