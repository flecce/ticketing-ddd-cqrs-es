﻿using Dapper;
using Infrastructure.Data.Interfaces;
using Infrastructure.Domain;
using MediatR;
using System;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Infrastructure.Data.Impl
{
    public class EventSourcingRepository<T> : IRepository<T> where T : AggregateBase
    {
        private readonly string _connectionString;
        private readonly IMediator _mediator;

        public EventSourcingRepository(
            string connectionString,
            IMediator mediator)
        {
            _connectionString = connectionString ?? throw new ArgumentException("connectionString");
            _mediator = mediator ?? throw new ArgumentException("mediator");
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            const string sql = "SELECT * FROM Events WHERE AggregateId=@id";
            using (var connection = new SqlConnection(_connectionString))
            {
                var rawEvents = await connection.QueryAsync<EventData>(sql, new { id });
                var events = rawEvents.Select(x => x.DeserializeEvent());
                var aggregate = CreateEmptyAggregate();

                foreach (var @event in events)
                {
                    aggregate.ApplyEvent((IDomainEvent)@event);
                }

                return aggregate;
            }
        }

        public async Task<bool> SaveAsync(T aggregate)
        {
            var events = aggregate.GetUncommittedEvents();
            if (events.Count() <= 0)
                return false;

            var aggregateType = aggregate.GetType().Name;
            var originalVersion = aggregate.Version - events.Count();
            var eventsToSave = events
                .Select(e => e.ToEventData(aggregateType, aggregate.Id, originalVersion++))
                .ToArray();

            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (var transaction = conn.BeginTransaction())
                {
                    var foundVersion = (int?)conn.ExecuteScalar(
                        "SELECT MAX(Version) FROM Events WHERE AggregateId=@aggregateId",
                        new { aggregateId = aggregate.Id },
                        transaction
                    );

                    if (foundVersion.HasValue && foundVersion >= originalVersion)
                        throw new Exception("Concurrency exception");

                    const string sql =
                        @"INSERT INTO Events(Id, Created, AggregateType, AggregateId, Version, Event, Metadata) 
                        VALUES(@Id, @Created, @AggregateType, @AggregateId, @Version, @Event, @Metadata)";
                    
                    await conn.ExecuteAsync(sql, eventsToSave, transaction);
                    transaction.Commit();
                }
            }

            // Publish domain events
            foreach (var @event in events)
                await _mediator.Publish(@event);

            aggregate.ClearUncommittedEvents();
            return true;
        }

        private T CreateEmptyAggregate()
        {
            return (T)typeof(T)
              .GetConstructor(
                BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public,
                null, new Type[0], new ParameterModifier[0])
              .Invoke(new object[0]);
        }
    }
}
