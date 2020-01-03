using Autofac;
using Infrastructure.Data.Impl;
using Infrastructure.Data.Interfaces;
using MediatR;
using System.Reflection;
using Ticketing.Api.Application.Commands;
using Ticketing.Api.Application.DomainEventHandlers;
using Ticketing.Api.Application.Queries.Interfaces;
using Ticketing.Api.Application.ReadModel.Impl;
using Ticketing.Api.Application.ReadModel.Models;
using Ticketing.Domain.Ticket;

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
            // Read repositories
            builder.RegisterType<MongoDbRepository<TicketReadModel>>()
               .As<IReadRepository<TicketReadModel>>();

            // Write repositories
            builder.RegisterType<EventSourcingRepository<Ticket>>()
               .As<IRepository<Ticket>>()
               .WithParameter("connectionString", _connectionString)
               .InstancePerLifetimeScope();

            // Readers
            builder.RegisterType<TicketReader>()
               .As<ITicketReader>()
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

            // Register the DomainEventHandler classes (they implement INotificationHandler<>) in assembly holding the Domain Events
            builder
                .RegisterAssemblyTypes(typeof(TicketCreatedDomainEventHandler).GetTypeInfo().Assembly)
                .AsClosedTypesOf(typeof(INotificationHandler<>));

            builder.Register<ServiceFactory>(ctx =>
            {
                var c = ctx.Resolve<IComponentContext>();
                return t => c.Resolve(t);
            });
        }
    }
}
