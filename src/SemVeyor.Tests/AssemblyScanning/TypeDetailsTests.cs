using System.Linq;
using SemVeyor.AssemblyScanning;
using SemVeyor.AssemblyScanning.Events;
using SemVeyor.Tests.Builder;
using Shouldly;
using Xunit;

namespace SemVeyor.Tests.AssemblyScanning
{
	public class TypeDetailsTests
	{
		[Fact]
		public void When_checking_the_log()
		{
			var first = Build.Type("first")
				.WithField(Build.Field<int>("_first"))
				.Build();

			var second = Build.Type("first")
				.WithField(Build.Field<int>("_first"))
				.Build();

			var change = first.UpdatedTo(second);

			change.ShouldBeEmpty();
		}

		[Fact]
		public void When_a_field_has_been_removed()
		{
			var first = Build.Type("first")
				.WithField(Build.Field<int>("_first"))
				.Build();

			var second = Build.Type("first")
				.Build();

			var change = first.UpdatedTo(second);

			change.Select(e => e.GetType()).ShouldBe(new[]
			{
				typeof(FieldRemoved)
			});
		}

		[Fact]
		public void When_a_field_has_been_added()
		{
			var first = Build.Type("first")
				.WithField(Build.Field<int>("_first"))
				.Build();

			var second = Build.Type("first")
				.WithField(Build.Field<int>("_first"))
				.WithField(Build.Field<int>("_second"))
				.Build();

			var change = first.UpdatedTo(second);

			change.Select(e => e.GetType()).ShouldBe(new[]
			{
				typeof(FieldAdded)
			});
		}

		[Fact]
		public void When_a_field_has_been_renamed()
		{
			var first = Build.Type("first")
				.WithField(Build.Field<int>("_first"))
				.Build();

			var second = Build.Type("first")
				.WithField(Build.Field<int>("_second"))
				.Build();

			var change = first.UpdatedTo(second);

			change.Select(e => e.GetType()).ShouldBe(new[]
			{
				typeof(FieldRemoved),
				typeof(FieldAdded)
			});
		}

		[Fact]
		public void When_a_field_has_becomes_less_visible()
		{
			var first = Build.Type("first")
				.WithField(Build.Field<int>("_first").WithVisibility(Visibility.Public))
				.Build();

			var second = Build.Type("first")
				.WithField(Build.Field<int>("_first"))
				.Build();

			var change = first.UpdatedTo(second);

			change.Select(e => e.GetType()).ShouldBe(new[]
			{
				typeof(FieldVisibilityDecreased),
			});
		}

		[Fact]
		public void When_a_field_has_becomes_more_visible()
		{
			var first = Build.Type("first")
				.WithField(Build.Field<int>("_first"))
				.Build();

			var second = Build.Type("first")
				.WithField(Build.Field<int>("_first").WithVisibility(Visibility.Public))
				.Build();

			var change = first.UpdatedTo(second);

			change.Select(e => e.GetType()).ShouldBe(new[]
			{
				typeof(FieldVisibilityIncreased),
			});
		}
	}
}
