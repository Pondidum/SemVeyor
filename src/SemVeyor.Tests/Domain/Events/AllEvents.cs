using System;
using System.Collections.Generic;
using System.Linq;
using SemVeyor.Domain.Events;
using Shouldly;
using Xunit;

namespace SemVeyor.Tests.Domain.Events
{
	public class AllEvents
	{
		private static readonly Type EventBase = typeof(IMajor);
		public static IEnumerable<object[]> Events => EventBase
			.Assembly
			.GetExportedTypes()
			.Where(t => t.Namespace == EventBase.Namespace)
			.Where(t => t.IsClass && t.IsAbstract == false)
			.Select(t => new object[] { t });

		[Theory]
		[MemberData("Events")]
		public void All_events_implement_either_IMajor_or_IMinor(Type eventType)
		{
			eventType.GetInterfaces().ShouldContain(t => t == typeof(IMajor) || t == typeof(IMinor));
		}

		[Theory]
		[MemberData("Events")]
		public void All_events_override_tostring(Type eventType)
		{
			var method = eventType.GetMethod(nameof(eventType.ToString));

			method.GetBaseDefinition().DeclaringType.ShouldNotBe(method.DeclaringType, () => $"{eventType.Name} does not override ToString()");
		}
	}
}
