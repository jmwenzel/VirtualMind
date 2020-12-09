using Autofac;
using MediatR;
using System.Reflection;

namespace VirtualMind.WebAPI.App.DependencyInjection
{
    using Cqs;

    /// <summary>
    /// Mediator module with Autofac
    /// </summary>
    public class MediatorModule : Autofac.Module
    {
        /// <inheritdoc />
        protected override void Load(ContainerBuilder builder)
        {
            // Register mediator
            builder.RegisterAssemblyTypes(typeof(IMediator)
                .GetTypeInfo().Assembly)
                .AsImplementedInterfaces();

            // Register all the Query classes (they implement IRequestHandler) in assembly holding the queries
            builder.RegisterAssemblyTypes(typeof(GetCurrencyExchangeQuery).GetTypeInfo().Assembly)
                .AsClosedTypesOf(typeof(IRequestHandler<,>));

            // Register all the Command classes (they implement IRequestHandler) in assembly holding the Commands
            builder.RegisterAssemblyTypes(typeof(CreateTransactionAsyncCmd).GetTypeInfo().Assembly)
                .AsClosedTypesOf(typeof(IRequestHandler<,>));

            builder.Register<ServiceFactory>(ctx =>
            {
                var c = ctx.Resolve<IComponentContext>();
                return t => c.Resolve(t);
            });

            // Behaviors
            builder.RegisterGeneric(typeof(LoggingBehavior<,>))
                .As(typeof(IPipelineBehavior<,>));
        }
    }
}
