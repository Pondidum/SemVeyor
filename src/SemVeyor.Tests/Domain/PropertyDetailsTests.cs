using System;
using System.Linq;
using SemVeyor.Domain;
using SemVeyor.Domain.Events;
using SemVeyor.Tests.Builder;
using Shouldly;
using Xunit;

namespace SemVeyor.Tests.Domain
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
		[InlineData(Visibility.Public, null, typeof(PropertyVisibilityDecreased))]
		[InlineData(Visibility.Public, Visibility.Internal, typeof(PropertyVisibilityDecreased))]
		[InlineData(Visibility.Public, Visibility.Private, typeof(PropertyVisibilityDecreased))]
		[InlineData(Visibility.Protected, Visibility.Public, typeof(PropertyVisibilityIncreased))]
		[InlineData(null, Visibility.Public, typeof(PropertyVisibilityIncreased))]
		public void When_the_setter_visibility_changes(Visibility? olderVisibility, Visibility? newerVisibility, Type expectedEvent)
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
			var newer = Build.Property<int>("one").WithArguments(Build.Parameter<string>("index")).Build();

			var changes = older.UpdatedTo(newer);

			changes.Select(c => c.GetType()).ShouldBe(new[]
			{
				typeof(PropertyArgumentAdded)
			});
		}

		[Fact]
		public void When_the_property_looses_an_indexer()
		{
			var older = Build.Property<int>("one").WithArguments(Build.Parameter<string>("index")).Build();
			var newer = Build.Property<int>("one").Build();

			var changes = older.UpdatedTo(newer);

			changes.Select(c => c.GetType()).ShouldBe(new[]
			{
				typeof(PropertyArgumentRemoved)
			});
		}

		[Fact]
		public void When_calling_tostring_with_no_setter()
		{
			var prop = Build.Property<int>("Count").WithVisibility(Visibility.Public).Build();

			prop.ToString().ShouldBe("Public System.Int32 Count { get; }");
		}

		[Fact]
		public void When_calling_tostring_with_a_setter_of_same_visibility()
		{
			var prop = Build
				.Property<int>("Count")
				.WithVisibility(Visibility.Public)
				.WithSetterVisibility(Visibility.Public)
				.Build();

			prop.ToString().ShouldBe("Public System.Int32 Count { get; set; }");
		}

		[Fact]
		public void When_calling_tostring_with_a_setter_of_different_visibility()
		{
			var prop = Build
				.Property<int>("Count")
				.WithVisibility(Visibility.Public)
				.WithSetterVisibility(Visibility.Internal)
				.Build();

			prop.ToString().ShouldBe("Public System.Int32 Count { get; Internal set; }");
		}

		[Fact]
		public void When_calling_tostring_with_an_indexer_argument()
		{
			var prop = Build
				.Property<string>("Ref")
				.WithVisibility(Visibility.Protected)
				.WithArguments(Build.Parameter<Guid>("id"))
				.Build();

			prop.ToString().ShouldBe("Protected System.String Ref[System.Guid id] { get; }");
		}

		[Fact]
		public void When_calling_tostring_with_multiple_indexer_arguments()
		{
			var prop = Build
				.Property<string>("Ref")
				.WithVisibility(Visibility.Protected)
				.WithArguments(
					Build.Parameter<int>("x"),
					Build.Parameter<int>("y"))
				.Build();

			prop.ToString().ShouldBe("Protected System.String Ref[System.Int32 x, System.Int32 y] { get; }");
		}
	}
}
