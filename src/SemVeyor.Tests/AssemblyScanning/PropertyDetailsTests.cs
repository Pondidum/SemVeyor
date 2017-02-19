using System;
using System.Linq;
using SemVeyor.AssemblyScanning;
using SemVeyor.AssemblyScanning.Events;
using SemVeyor.Tests.Builder;
using Shouldly;
using Xunit;

namespace SemVeyor.Tests.AssemblyScanning
{
	public class PropertyDetailsTests
	{
		[Theory]
		[InlineData(Visibility.Public, Visibility.Protected, typeof(PropertyVisibilityDecreased))]
		[InlineData(Visibility.Public, Visibility.Internal, typeof(PropertyVisibilityDecreased))]
		[InlineData(Visibility.Public, Visibility.Private, typeof(PropertyVisibilityDecreased))]
		[InlineData(Visibility.Protected, Visibility.Public, typeof(PropertyVisibilityIncreased))]
		public void When_the_visibility_changes(Visibility olderVisibility, Visibility newerVisibility, Type expectedEvent)
		{
			var older = Build.Property<int>("prop").WithVisibility(olderVisibility).Build();
			var newer = Build.Property<int>("prop").WithVisibility(newerVisibility).Build();

			var changes = older.UpdatedTo(newer);

			changes.Select(c => c.GetType()).ShouldBe(new[]
			{
				expectedEvent
			});
		}

		[Theory]
		[InlineData(Visibility.Public, Visibility.Protected, typeof(PropertyVisibilityDecreased))]
		[InlineData(Visibility.Public, Visibility.Internal, typeof(PropertyVisibilityDecreased))]
		[InlineData(Visibility.Public, Visibility.Private, typeof(PropertyVisibilityDecreased))]
		[InlineData(Visibility.Protected, Visibility.Public, typeof(PropertyVisibilityIncreased))]
		public void When_the_setter_visibility_changes(Visibility olderVisibility, Visibility newerVisibility, Type expectedEvent)
		{
			var older = Build.Property<int>("prop").WithSetterVisibility(olderVisibility).Build();
			var newer = Build.Property<int>("prop").WithSetterVisibility(newerVisibility).Build();

			var changes = older.UpdatedTo(newer);

			changes.Select(c => c.GetType()).ShouldBe(new[]
			{
				expectedEvent
			});
		}

		[Fact]
		public void When_the_type_changes()
		{
			var older = Build.Property<int>("one").Build();
			var newer = Build.Property<string>("one").Build();

			var changes = older.UpdatedTo(newer);

			changes.Select(c => c.GetType()).ShouldBe(new[]
			{
				typeof(PropertyTypeChanged)
			});
		}

		[Fact]
		public void When_the_property_gains_an_indexer()
		{
			var older = Build.Property<int>("one").Build();
			var newer = Build.Property<int>("one").WithArguments(Build.Argument<string>("index")).Build();

			var changes = older.UpdatedTo(newer);

			changes.Select(c => c.GetType()).ShouldBe(new[]
			{
				typeof(PropertyArgumentAdded)
			});
		}

		[Fact]
		public void When_the_property_looses_an_indexer()
		{
			var older = Build.Property<int>("one").WithArguments(Build.Argument<string>("index")).Build();
			var newer = Build.Property<int>("one").Build();

			var changes = older.UpdatedTo(newer);

			changes.Select(c => c.GetType()).ShouldBe(new[]
			{
				typeof(PropertyArgumentRemoved)
			});
		}
	}
}
