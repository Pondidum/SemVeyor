using System;
using System.Collections.Generic;
using System.Linq;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Kernel;
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
		[MemberData(nameof(Events))]
		public void All_events_implement_either_IMajor_or_IMinor(Type eventType)
		{
			eventType.GetInterfaces().ShouldContain(t => t == typeof(IMajor) || t == typeof(IMinor));
		}

		[Theory]
		[MemberData(nameof(Events))]
		public void All_events_override_tostring(Type eventType)
		{
			var method = eventType.GetMethod(nameof(eventType.ToString));

			method.GetBaseDefinition().DeclaringType.ShouldNotBe(method.DeclaringType, () => $"{eventType.Name} does not override ToString()");
		}

		[Theory]
		[MemberData(nameof(Events))]
		public void All_events_implement_tostring(Type eventType)
		{
			var instance = Build(eventType).ToString();

			instance.ShouldSatisfyAllConditions(
				() => instance.ShouldNotBe(eventType.FullName),
				() => instance.ShouldStartWith(eventType.Name)
			);
		}

		private object Build(Type type)
		{
			var ctor = type.GetConstructors().Single();

			if (ctor.GetParameters().Any() == false)
				return ctor.Invoke(new object[0]);

			var fixture = new SpecimenContext(new Fixture());
			var args = ctor.GetParameters().Select(p => fixture.Resolve(p));

			return ctor.Invoke(args.ToArray());
		}
	}
}
