using Autofac;
using Domain.Ticket;
using Infrastructure.Data.Impl;
using Infrastructure.Data.Interfaces;

namespace Ticketing.Api.Infrastructure
{
    public class ApplicationModule : Module
    {
        private readonly string _connectionString;

        public ApplicationModule(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => new EventSourcingRepository<Ticket>(_connectionString))
                .As<IRepository<Ticket>>()
                .InstancePerLifetimeScope();
        }
    }
}
