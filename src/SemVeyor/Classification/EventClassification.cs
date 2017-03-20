using System;
using System.Collections.Generic;
using System.Linq;
using SemVeyor.Domain.Events;

namespace SemVeyor.Classification
{
	public class EventClassification
	{
		private static readonly Type Major = typeof(IMajor);
		private static readonly Type Minor = typeof(IMinor);

		public SemVer ClassifyEvent(object @event)
		{
			var eventType = @event as Type ?? @event.GetType();
			var interfaces = eventType.GetInterfaces();

			if (interfaces.Contains(Major))
				return SemVer.Major;

			if (interfaces.Contains(Minor))
				return SemVer.Minor;

			return SemVer.None;
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
