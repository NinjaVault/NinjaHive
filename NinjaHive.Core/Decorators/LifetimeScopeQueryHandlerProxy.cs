using System;
using SimpleInjector;

namespace NinjaHive.Core.Decorators
{
    public class LifetimeScopeQueryHandlerProxy<TQuery, TResult>
        : IQueryHandler<TQuery, TResult> where TQuery : IQuery<TResult>
    {
        private readonly Container container;
        private readonly Func<IQueryHandler<TQuery, TResult>> decorateeFactory;

        public LifetimeScopeQueryHandlerProxy(Container container,
            Func<IQueryHandler<TQuery, TResult>> decorateeFactory)
        {
            this.container = container;
            this.decorateeFactory = decorateeFactory;
        }

        public TResult Handle(TQuery query)
        {
            // Start a new scope.
            using (container.BeginLifetimeScope())
            {
                // Create the decorateeFactory within the scope.
                IQueryHandler<TQuery, TResult> handler = this.decorateeFactory.Invoke();
                return handler.Handle(query);
            };
        }
    }
}
