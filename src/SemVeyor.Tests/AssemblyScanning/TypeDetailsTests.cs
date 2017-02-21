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

		[Fact]
		public void When_a_method_has_been_added()
		{
			var first = Build.Type("").Build();
			var second = Build.Type("").WithMethods(Build.Method("One")).Build();

			var changes = first.UpdatedTo(second);

			changes.Select(c => c.GetType()).ShouldBe(new []
			{
				typeof(MethodAdded)
			});
		}

		[Fact]
		public void When_a_method_has_been_removed()
		{
			var first = Build.Type("").WithMethods(Build.Method("One")).Build();
			var second = Build.Type("").Build();

			var changes = first.UpdatedTo(second);

			changes.Select(c => c.GetType()).ShouldBe(new []
			{
				typeof(MethodRemoved)
			});
		}

		[Fact]
		public void When_a_method_has_become_more_visible()
		{
			var first = Build.Type("").WithMethods(Build.Method("One")).Build();
			var second = Build.Type("").WithMethods(Build.Method("One").WithVisibility(Visibility.Public)).Build();

			var changes = first.UpdatedTo(second);

			changes.Select(c => c.GetType()).ShouldBe(new []
			{
				typeof(MethodVisibilityIncreased)
			});
		}

		[Fact]
		public void When_a_method_has_become_less_visible()
		{
			var first = Build.Type("").WithMethods(Build.Method("One").WithVisibility(Visibility.Public)).Build();
			var second = Build.Type("").WithMethods(Build.Method("One")).Build();

			var changes = first.UpdatedTo(second);

			changes.Select(c => c.GetType()).ShouldBe(new []
			{
				typeof(MethodVisibilityDecreased)
			});
		}

		[Fact]
		public void When_a_method_overload_is_added()
		{
			var first = Build.Type("")
				.WithMethods(Build.Method("One"))
				.Build();

			var second = Build.Type("")
				.WithMethods(Build.Method("One"))
				.WithMethods(Build.Method("One").WithArguments(Build.Argument<int>("value")))
				.Build();

			var changes = first.UpdatedTo(second);

			changes.Select(c => c.GetType()).ShouldBe(new[]
			{
				typeof(MethodAdded)
			});
		}

		[Fact]
		public void When_a_method_overload_is_removed()
		{
			var first = Build.Type("")
				.WithMethods(Build.Method("One"))
				.WithMethods(Build.Method("One").WithArguments(Build.Argument<int>("value")))
				.Build();

			var second = Build.Type("")
				.WithMethods(Build.Method("One"))
				.Build();

			var changes = first.UpdatedTo(second);

			changes.Select(c => c.GetType()).ShouldBe(new[]
			{
				typeof(MethodRemoved)
			});
		}

		[Fact]
		public void When_a_property_has_been_added()
		{
			var older = Build.Type("").WithProperties(Build.Property<int>("Prop")).Build();
			var newer = Build.Type("").WithProperties(Build.Property<int>("Prop"), Build.Property<string>("Other")).Build();

			var changes = older.UpdatedTo(newer);

			changes.Select(c => c.GetType()).ShouldBe(new []
			{
				typeof(PropertyAdded)
			});
		}

		[Fact]
		public void When_a_property_has_been_removed()
		{
			var older = Build.Type("").WithProperties(Build.Property<int>("Prop"), Build.Property<string>("Other")).Build();
			var newer = Build.Type("").WithProperties(Build.Property<int>("Prop")).Build();

			var changes = older.UpdatedTo(newer);

			changes.Select(c => c.GetType()).ShouldBe(new []
			{
				typeof(PropertyRemoved)
			});
		}

		[Fact]
		public void When_a_property_has_been_renamed()
		{
			var older = Build.Type("").WithProperties(Build.Property<int>("Prop")).Build();
			var newer = Build.Type("").WithProperties(Build.Property<int>("NewProp")).Build();

			var changes = older.UpdatedTo(newer);

			changes.Select(c => c.GetType()).ShouldBe(new []
			{
				typeof(PropertyRemoved),
				typeof(PropertyAdded)
			});
		}

		[Fact]
		public void When_a_property_overload_is_added()
		{
			var older = Build.Type("").WithProperties(Build.Property<int>("Prop")).Build();
			var newer = Build.Type("").WithProperties(Build.Property<int>("Prop"), Build.Property<string>("PropTwo")).Build();

			var changes = older.UpdatedTo(newer);

			changes.Select(c => c.GetType()).ShouldBe(new []
			{
				typeof(PropertyAdded)
			});
		}

		[Fact]
		public void When_a_property_overload_is_removed()
		{
			var older = Build.Type("").WithProperties(Build.Property<int>("Prop"), Build.Property<string>("PropTwo")).Build();
			var newer = Build.Type("").WithProperties(Build.Property<int>("Prop")).Build();

			var changes = older.UpdatedTo(newer);

			changes.Select(c => c.GetType()).ShouldBe(new []
			{
				typeof(PropertyRemoved)
			});
		}
	}
}
