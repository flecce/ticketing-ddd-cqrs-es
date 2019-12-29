using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace Infrastructure.Domain
{
	public static class EventDataExtensions
	{
		private static readonly JsonSerializerSettings SerializerSettings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.None };

		public static EventData ToEventData(this object @event, string aggregateType, Guid aggregateId, int version)
		{
			var data = JsonConvert.SerializeObject(@event, SerializerSettings);
			var eventHeaders = new Dictionary<string, object>
			{
				{
					"EventClrType", @event.GetType().AssemblyQualifiedName
				}
			};

			var metadata = JsonConvert.SerializeObject(eventHeaders, SerializerSettings);
			var eventId = Guid.NewGuid(); //CombGuid.Generate();

			return new EventData
			{
				Id = eventId,
				Created = DateTime.Now,
				AggregateType = aggregateType,
				AggregateId = aggregateId,
				Version = version,
				Event = data,
				Metadata = metadata,
			};
		}

		public static object DeserializeEvent(this EventData x)
		{
			var eventClrTypeName = JObject.Parse(x.Metadata).Property("EventClrType").Value;
			return JsonConvert.DeserializeObject(x.Event, Type.GetType((string)eventClrTypeName));
		}
	}
}
