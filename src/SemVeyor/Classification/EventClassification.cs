using System;
using System.Collections.Generic;
using System.Linq;

namespace SemVeyor.Classification
{
	public class EventClassification
	{
		private readonly IDictionary<string, SemVer> _classificationMap;

		public EventClassification(IDictionary<string, SemVer> classificationMap)
		{
			_classificationMap = classificationMap;
		}

		public SemVer ClassifyEvent(object @event)
		{
			var eventType = @event as Type ?? @event.GetType();

			SemVer version;
			return _classificationMap.TryGetValue(eventType.Name, out version)
				? version
				: SemVer.None;
		}

		public IEnumerable<ChangeClassification> ClassifyAll(IEnumerable<object> events)
		{
			return events.Select(@event => new ChangeClassification
			{
				Change = @event,
				Classification = ClassifyEvent(@event)
			});
		}
	}
}
