using System;
using SimpleInjector;

namespace NinjaHive.Core.Decorators
{
    public class LifetimeScopeCommandHandlerProxy<T> : ICommandHandler<T>
    {
        private readonly Container container;
        private readonly Func<ICommandHandler<T>> decorateeFactory;

        public LifetimeScopeCommandHandlerProxy(Container container,
            Func<ICommandHandler<T>> decorateeFactory)
        {
            this.container = container;
            this.decorateeFactory = decorateeFactory;
        }

        public void Handle(T command)
        {
            // Start a new scope.
            using (container.BeginLifetimeScope())
            {
                // Create the decorateeFactory within the scope.
                ICommandHandler<T> handler = this.decorateeFactory.Invoke();
                handler.Handle(command);
            };
        }
    }
}
