using SemVeyor.Classification;
using SemVeyor.Domain.Events;
using Shouldly;
using System;
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
		[InlineData(typeof(FieldAdded), SemVer.Minor)]
		[InlineData(typeof(FieldRemoved), SemVer.Major)]
		[InlineData(typeof(MethodAdded), SemVer.Minor)]
		[InlineData(typeof(MethodRemoved), SemVer.Major)]
		[InlineData(typeof(PropertyAdded), SemVer.Minor)]
		[InlineData(typeof(PropertyRemoved), SemVer.Major)]
		[InlineData(typeof(CtorAdded), SemVer.Minor)]
		[InlineData(typeof(CtorRemoved), SemVer.Major)]
		[InlineData(typeof(GenericArgumentAdded), SemVer.Minor)]
		[InlineData(typeof(GenericArgumentRemoved), SemVer.Major)]

		[InlineData(typeof(FieldVisibilityIncreased), SemVer.Minor)]
		[InlineData(typeof(FieldVisibilityDecreased), SemVer.Major)]
		[InlineData(typeof(FieldTypeChanged), SemVer.Major)]

		[InlineData(typeof(GenericArgumentPositionChanged), SemVer.Major)]
		[InlineData(typeof(GenericArgumentNameChanged), SemVer.Major)]
		[InlineData(typeof(GenericArgumentConstraintAdded), SemVer.Minor)]
		[InlineData(typeof(GenericArgumentConstraintRemoved), SemVer.Major)]

		[InlineData(typeof(ArgumentNameChanged), SemVer.Major)]
		[InlineData(typeof(ArgumentTypeChanged), SemVer.Major)]
		[InlineData(typeof(ArgumentMoved), SemVer.Major)]

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
		public void All_events_are_classified(Type eventType, SemVer version)
		{
			new EventClassification()
				.ClassifyEvent(eventType)
				.ShouldBe(version, () => $"{eventType.Name} should be {version}");
		}
	}
}