using System;
using System.Linq;
using SemVeyor.Domain.Events;

namespace SemVeyor.Classification
{
	public enum SemVer
	{
		None,
		Major,
		Minor,
		Patch
	}

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
	}
}
