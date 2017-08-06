using SemVeyor.Classification;
using SemVeyor.Domain.Events;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace SemVeyor.Tests.Classification
{
	public class EventClassificationTests
	{

		[Theory]
		[InlineData(typeof(AssemblyTypeAdded), SemVer.Minor)]
		[InlineData(typeof(AssemblyTypeRemoved), SemVer.Major)]

		[InlineData(typeof(TypeVisibilityIncreased), SemVer.Minor)]
		[InlineData(typeof(TypeVisibilityDecreased), SemVer.Major)]
		[InlineData(typeof(TypeFieldAdded), SemVer.Minor)]
		[InlineData(typeof(TypeFieldRemoved), SemVer.Major)]
		[InlineData(typeof(TypeMethodAdded), SemVer.Minor)]
		[InlineData(typeof(TypeMethodRemoved), SemVer.Major)]
		[InlineData(typeof(TypePropertyAdded), SemVer.Minor)]
		[InlineData(typeof(TypePropertyRemoved), SemVer.Major)]
		[InlineData(typeof(TypeCtorAdded), SemVer.Minor)]
		[InlineData(typeof(TypeCtorRemoved), SemVer.Major)]
		[InlineData(typeof(TypeGenericArgumentAdded), SemVer.Minor)]
		[InlineData(typeof(TypeGenericArgumentRemoved), SemVer.Major)]

		[InlineData(typeof(FieldVisibilityIncreased), SemVer.Minor)]
		[InlineData(typeof(FieldVisibilityDecreased), SemVer.Major)]
		[InlineData(typeof(FieldTypeChanged), SemVer.Major)]

		[InlineData(typeof(GenericArgumentPositionChanged), SemVer.Major)]
		[InlineData(typeof(GenericArgumentNameChanged), SemVer.Major)]
		[InlineData(typeof(GenericArgumentConstraintAdded), SemVer.Minor)]
		[InlineData(typeof(GenericArgumentConstraintRemoved), SemVer.Major)]

		[InlineData(typeof(ParameterNameChanged), SemVer.Major)]
		[InlineData(typeof(ParameterTypeChanged), SemVer.Major)]
		[InlineData(typeof(ParameterMoved), SemVer.Major)]

		[InlineData(typeof(MethodVisibilityIncreased), SemVer.Minor)]
		[InlineData(typeof(MethodVisibilityDecreased), SemVer.Major)]
		[InlineData(typeof(MethodNameChanged), SemVer.Major)]
		[InlineData(typeof(MethodTypeChanged), SemVer.Major)]
		[InlineData(typeof(MethodArgumentAdded), SemVer.Minor)]
		[InlineData(typeof(MethodArgumentRemoved), SemVer.Major)]
		[InlineData(typeof(MethodGenericArgumentAdded), SemVer.Minor)]
		[InlineData(typeof(MethodGenericArgumentRemoved), SemVer.Major)]

		[InlineData(typeof(PropertyVisibilityDecreased), SemVer.Major)]
		[InlineData(typeof(PropertyVisibilityIncreased), SemVer.Minor)]
		[InlineData(typeof(PropertyTypeChanged), SemVer.Major)]
		[InlineData(typeof(PropertyArgumentAdded), SemVer.Minor)]
		[InlineData(typeof(PropertyArgumentRemoved), SemVer.Major)]

		[InlineData(typeof(CtorVisibilityDecreased), SemVer.Major)]
		[InlineData(typeof(CtorVisibilityIncreased), SemVer.Minor)]
		[InlineData(typeof(CtorArgumentAdded), SemVer.Minor)]
		[InlineData(typeof(CtorArgumentRemoved), SemVer.Major)]
		public void All_events_are_classified_correctly(Type eventType, SemVer version)
		{
			new EventClassification(EventClassification.DefaultClassificationMap)
				.ClassifyEvent(eventType)
				.ShouldBe(version, () => $"{eventType.Name} should be {version}");
		}

		[Theory]
		[MemberData(nameof(Events))]
		public void All_events_have_a_classification(object @event)
		{
			new EventClassification(EventClassification.DefaultClassificationMap)
				.ClassifyEvent(@event)
				.ShouldNotBe(SemVer.None);
		}

		private static readonly Type EventBase = typeof(AssemblyTypeAdded);
		public static IEnumerable<object[]> Events => EventBase
			.Assembly
			.GetExportedTypes()
			.Where(t => t.Namespace == EventBase.Namespace)
			.Where(t => t.IsClass && t.IsAbstract == false)
			.Select(t => new object[] { t });
	}
}