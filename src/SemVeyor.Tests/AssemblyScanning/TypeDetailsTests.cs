using System;
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
		private static void ChangesShouldBe(TypeDetails older, TypeDetails newer, params Type[] expected)
		{
			older
				.UpdatedTo(newer)
				.Select(c => c.GetType())
				.ShouldBe(expected);
		}

		[Fact]
		public void When_checking_the_log()
		{
			var older = Build.Type("first")
				.WithField(Build.Field<int>("_first"))
				.Build();

			var newer = Build.Type("first")
				.WithField(Build.Field<int>("_first"))
				.Build();

			ChangesShouldBe(older, newer);
		}

		[Fact]
		public void When_a_field_has_been_removed()
		{
			var older = Build.Type("first")
				.WithField(Build.Field<int>("_first"))
				.Build();

			var newer = Build.Type("first")
				.Build();

			ChangesShouldBe(older, newer, typeof(FieldRemoved));
		}

		[Fact]
		public void When_a_field_has_been_added()
		{
			var older = Build.Type("first")
				.WithField(Build.Field<int>("_first"))
				.Build();

			var newer = Build.Type("first")
				.WithField(Build.Field<int>("_first"))
				.WithField(Build.Field<int>("_second"))
				.Build();

			ChangesShouldBe(older, newer, typeof(FieldAdded));
		}

		[Fact]
		public void When_a_field_has_been_renamed()
		{
			var older = Build.Type("first")
				.WithField(Build.Field<int>("_first"))
				.Build();

			var newer = Build.Type("first")
				.WithField(Build.Field<int>("_second"))
				.Build();

			ChangesShouldBe(older, newer, typeof(FieldRemoved),
				typeof(FieldAdded));
		}

		[Fact]
		public void When_a_field_has_becomes_less_visible()
		{
			var older = Build.Type("first")
				.WithField(Build.Field<int>("_first").WithVisibility(Visibility.Public))
				.Build();

			var newer = Build.Type("first")
				.WithField(Build.Field<int>("_first"))
				.Build();

			ChangesShouldBe(older, newer, typeof(FieldVisibilityDecreased));
		}

		[Fact]
		public void When_a_field_has_becomes_more_visible()
		{
			var older = Build.Type("first")
				.WithField(Build.Field<int>("_first"))
				.Build();

			var newer = Build.Type("first")
				.WithField(Build.Field<int>("_first").WithVisibility(Visibility.Public))
				.Build();

			ChangesShouldBe(older, newer, typeof(FieldVisibilityIncreased));
		}

		[Fact]
		public void When_a_method_has_been_added()
		{
			var older = Build.Type("").Build();
			var newer = Build.Type("").WithMethods(Build.Method("One")).Build();

			ChangesShouldBe(older, newer, typeof(MethodAdded));
		}

		[Fact]
		public void When_a_method_has_been_removed()
		{
			var older = Build.Type("").WithMethods(Build.Method("One")).Build();
			var newer = Build.Type("").Build();

			ChangesShouldBe(older, newer, typeof(MethodRemoved));
		}

		[Fact]
		public void When_a_method_has_become_more_visible()
		{
			var older = Build.Type("").WithMethods(Build.Method("One")).Build();
			var newer = Build.Type("").WithMethods(Build.Method("One").WithVisibility(Visibility.Public)).Build();

			ChangesShouldBe(older, newer, typeof(MethodVisibilityIncreased));
		}

		[Fact]
		public void When_a_method_has_become_less_visible()
		{
			var older = Build.Type("").WithMethods(Build.Method("One").WithVisibility(Visibility.Public)).Build();
			var newer = Build.Type("").WithMethods(Build.Method("One")).Build();

			ChangesShouldBe(older, newer, typeof(MethodVisibilityDecreased));
		}

		[Fact]
		public void When_a_method_overload_is_added()
		{
			var older = Build.Type("")
				.WithMethods(Build.Method("One"))
				.Build();

			var newer = Build.Type("")
				.WithMethods(Build.Method("One"))
				.WithMethods(Build.Method("One").WithArguments(Build.Argument<int>("value")))
				.Build();

			ChangesShouldBe(older, newer, typeof(MethodAdded));
		}

		[Fact]
		public void When_a_method_overload_is_removed()
		{
			var older = Build.Type("")
				.WithMethods(Build.Method("One"))
				.WithMethods(Build.Method("One").WithArguments(Build.Argument<int>("value")))
				.Build();

			var newer = Build.Type("")
				.WithMethods(Build.Method("One"))
				.Build();

			ChangesShouldBe(older, newer, typeof(MethodRemoved));
		}

		[Fact]
		public void When_a_property_has_been_added()
		{
			var older = Build.Type("").WithProperties(Build.Property<int>("Prop")).Build();
			var newer = Build.Type("").WithProperties(Build.Property<int>("Prop"), Build.Property<string>("Other")).Build();

			var changes = older.UpdatedTo(newer);

			ChangesShouldBe(older, newer, typeof(PropertyAdded));
		}

		[Fact]
		public void When_a_property_has_been_removed()
		{
			var older = Build.Type("").WithProperties(Build.Property<int>("Prop"), Build.Property<string>("Other")).Build();
			var newer = Build.Type("").WithProperties(Build.Property<int>("Prop")).Build();

			ChangesShouldBe(older, newer, typeof(PropertyRemoved));
		}

		[Fact]
		public void When_a_property_has_been_renamed()
		{
			var older = Build.Type("").WithProperties(Build.Property<int>("Prop")).Build();
			var newer = Build.Type("").WithProperties(Build.Property<int>("NewProp")).Build();

			ChangesShouldBe(older, newer, typeof(PropertyRemoved), typeof(PropertyAdded));
		}

		[Fact]
		public void When_a_property_overload_is_added()
		{
			var older = Build.Type("").WithProperties(Build.Property<int>("Prop")).Build();
			var newer = Build.Type("").WithProperties(Build.Property<int>("Prop"), Build.Property<string>("PropTwo")).Build();

			ChangesShouldBe(older, newer, typeof(PropertyAdded));
		}

		[Fact]
		public void When_a_property_overload_is_removed()
		{
			var older = Build.Type("").WithProperties(Build.Property<int>("Prop"), Build.Property<string>("PropTwo")).Build();
			var newer = Build.Type("").WithProperties(Build.Property<int>("Prop")).Build();

			ChangesShouldBe(older, newer, typeof(PropertyRemoved));
		}
	}
}
