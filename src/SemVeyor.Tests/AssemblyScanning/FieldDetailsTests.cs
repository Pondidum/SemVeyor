using System;
using System.Collections.Generic;
using System.Linq;
using SemVeyor.AssemblyScanning;
using SemVeyor.AssemblyScanning.Events;
using Shouldly;
using Xunit;

namespace SemVeyor.Tests.AssemblyScanning
{
	public class FieldDetailsTests
	{
		[Fact]
		public void When_checking_the_log()
		{
			var first = Build.Field<int>("first").Build();
			var second = Build.Field<int>("first").Build();

			var change = first.UpdatedTo(second);

			change.ShouldBeEmpty();
		}

		[Fact]
		public void When_the_types_are_different()
		{
			var first = Build.Field<int>("first").Build();
			var second = Build.Field<string>("first").Build();

			var changes = first.UpdatedTo(second);

			changes.Select(c => c.GetType()).ShouldBe(new[]
			{
				typeof(FieldTypeChanged),
			});
		}

		[Theory]
		[InlineData(Visibility.Public, Visibility.Protected, typeof(FieldVisibilityDecreased))]
		[InlineData(Visibility.Public, Visibility.Internal, typeof(FieldVisibilityDecreased))]
		[InlineData(Visibility.Public, Visibility.Private, typeof(FieldVisibilityDecreased))]
		[InlineData(Visibility.Protected, Visibility.Public, typeof(FieldVisibilityIncreased))]
		public void When_the_type_visibility_changes(Visibility older, Visibility newer, Type expectedEvent)
		{
			var first = Build.Field<int>("first").WithVisibility(older).Build();
			var second = Build.Field<int>("first").WithVisibility(newer).Build();

			var changes = first.UpdatedTo(second);

			changes.Select(c => c.GetType()).ShouldBe(new[]
			{
				expectedEvent,
			});
		}
	}
}