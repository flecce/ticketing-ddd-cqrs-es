using Autofac;
using Ticketing.Domain.Ticket;
using Infrastructure.Data.Impl;
using Infrastructure.Data.Interfaces;
using MediatR;
using System.Reflection;
using Ticketing.Api.Application.Commands;

namespace Ticketing.Api.Infrastructure
{
    public class ApplicationModule : Autofac.Module
    {
        private readonly string _connectionString;

        public ApplicationModule(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder
                .Register(c => new EventSourcingRepository<Ticket>(_connectionString))
                .As<IRepository<Ticket>>()
                .InstancePerLifetimeScope();

            // Mediator
            builder
                .RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly)
                .AsImplementedInterfaces();

            // Register all the Command classes (they implement IRequestHandler) in assembly
            // holding the Commands
            builder
                .RegisterAssemblyTypes(typeof(CreateTicketCommand).GetTypeInfo().Assembly)
                .AsClosedTypesOf(typeof(IRequestHandler<,>));

            builder.Register<ServiceFactory>(ctx =>
            {
                var c = ctx.Resolve<IComponentContext>();
                return t => c.Resolve(t);
            });
        }
    }
}
