using Castle.Windsor;

namespace Insperity.Integration.Trucking.Core
{
    public static class IoC
    {
        private static IWindsorContainer _container;

        public static IWindsorContainer Container
        {
            get => _container ?? (_container = new WindsorContainer());
            set => _container = value;
        }
    }
}
