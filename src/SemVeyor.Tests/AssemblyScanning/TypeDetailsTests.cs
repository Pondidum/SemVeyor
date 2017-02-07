using System.Linq;
using SemVeyor.AssemblyScanning;
using SemVeyor.AssemblyScanning.Events;
using Shouldly;
using Xunit;

namespace SemVeyor.Tests.AssemblyScanning
{
	public class TypeDetailsTests
	{
		[Fact]
		public void When_checking_the_log()
		{
			var first = TypeWith(FieldFrom<int>("first", Visibility.Public));
			var second = TypeWith(FieldFrom<int>("first", Visibility.Public));

			var change = first.UpdatedTo(second);

			change.ShouldBeEmpty();
		}

		[Fact]
		public void When_a_field_has_been_removed()
		{
			var first = TypeWith(FieldFrom<int>("first", Visibility.Public));
			var second = TypeWith();

			var change = first.UpdatedTo(second);

			change.Select(e => e.GetType()).ShouldBe(new[]
			{
				typeof(FieldRemoved)
			});
		}

		[Fact]
		public void When_a_field_has_been_added()
		{
			var first = TypeWith(FieldFrom<int>("first", Visibility.Public));
			var second = TypeWith(FieldFrom<int>("first", Visibility.Public), FieldFrom<string>("second", Visibility.Public));

			var change = first.UpdatedTo(second);

			change.Select(e => e.GetType()).ShouldBe(new[]
			{
				typeof(FieldAdded)
			});
		}

		[Fact]
		public void When_a_field_has_been_renamed()
		{
			var first = TypeWith(FieldFrom<int>("first", Visibility.Public));
			var second = TypeWith(FieldFrom<int>("second", Visibility.Public));

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
			var first = TypeWith(FieldFrom<int>("first", Visibility.Public));
			var second = TypeWith(FieldFrom<int>("first", Visibility.Private));

			var change = first.UpdatedTo(second);

			change.Select(e => e.GetType()).ShouldBe(new[]
			{
				typeof(FieldVisibilityDecreased),
			});
		}

		[Fact]
		public void When_a_field_has_becomes_more_visible()
		{
			var first = TypeWith(FieldFrom<int>("first", Visibility.Protected));
			var second = TypeWith(FieldFrom<int>("first", Visibility.Public));

			var change = first.UpdatedTo(second);

			change.Select(e => e.GetType()).ShouldBe(new[]
			{
				typeof(FieldVisibilityIncreased),
			});
		}

		private static TypeDetails TypeWith(params FieldDetails[] fields)
		{
			return new TypeDetails
			{
				Name = "first-type",
				Visibility = Visibility.Public,
				Fields = fields
			};
		}

		private static FieldDetails FieldFrom<T>(string name, Visibility visibility)
		{
			return new FieldDetails
			{
				Name = name,
				Visibility = visibility,
				Type = typeof(T)
			};
		}
	}
}
