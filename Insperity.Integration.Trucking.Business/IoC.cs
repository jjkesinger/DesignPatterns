using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.Windsor;
using Insperity.Integration.Trucking.Business.Clients;
using Insperity.Integration.Trucking.Business.Events;
using Insperity.Integration.Trucking.Business.Events.Handling;
using Insperity.Integration.Trucking.Business.Providers;
using Insperity.Integration.Trucking.Core;

namespace Insperity.Integration.Trucking.Business
{
    public static class IoC
    {
        public static void InitBusiness(IWindsorContainer container)
        {
            container.Kernel.Resolver.AddSubResolver(new CollectionResolver(container.Kernel, true));
            container.Register(
                Component.For<ILogger>().ImplementedBy<InsperityLogger>().LifeStyle.Transient,
                Component.For<IHttpClientFactory>().ImplementedBy<HttpClientFactory>().LifeStyle.Transient,
                Component.For<ISubscriptionService>().ImplementedBy<EventSubscriptions>().LifeStyle.Transient,
                Component.For<IEventPublisher>().ImplementedBy<EventPublisher>().LifeStyle.Transient,
                Classes.FromThisAssembly().BasedOn(typeof(IntegrationStore)).WithServiceFromInterface().LifestyleTransient(),
                Classes.FromThisAssembly().BasedOn(typeof(IHandle<>)).WithServiceAllInterfaces().LifestyleTransient()
            );
        }
    }
}
