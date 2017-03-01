using System;
using System.Linq;
using SemVeyor.Domain;
using SemVeyor.Domain.Events;
using SemVeyor.Tests.Builder;
using Shouldly;
using Xunit;

namespace SemVeyor.Tests.Domain
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
			var older = Build.Type("older")
				.WithField(Build.Field<int>("_older"))
				.Build();

			var newer = Build.Type("older")
				.WithField(Build.Field<int>("_older"))
				.Build();

			ChangesShouldBe(older, newer);
		}

		[Fact]
		public void When_a_field_has_been_removed()
		{
			var older = Build.Type("older")
				.WithField(Build.Field<int>("_older"))
				.Build();

			var newer = Build.Type("older")
				.Build();

			ChangesShouldBe(older, newer, typeof(FieldRemoved));
		}

		[Fact]
		public void When_a_field_has_been_added()
		{
			var older = Build.Type("older")
				.WithField(Build.Field<int>("_older"))
				.Build();

			var newer = Build.Type("older")
				.WithField(Build.Field<int>("_older"))
				.WithField(Build.Field<int>("_newer"))
				.Build();

			ChangesShouldBe(older, newer, typeof(FieldAdded));
		}

		[Fact]
		public void When_a_field_has_been_renamed()
		{
			var older = Build.Type("older")
				.WithField(Build.Field<int>("_older"))
				.Build();

			var newer = Build.Type("older")
				.WithField(Build.Field<int>("_newer"))
				.Build();

			ChangesShouldBe(older, newer, typeof(FieldRemoved),
				typeof(FieldAdded));
		}

		[Fact]
		public void When_a_field_has_becomes_less_visible()
		{
			var older = Build.Type("older")
				.WithField(Build.Field<int>("_older").WithVisibility(Visibility.Public))
				.Build();

			var newer = Build.Type("older")
				.WithField(Build.Field<int>("_older"))
				.Build();

			ChangesShouldBe(older, newer, typeof(FieldVisibilityDecreased));
		}

		[Fact]
		public void When_a_field_has_becomes_more_visible()
		{
			var older = Build.Type("older")
				.WithField(Build.Field<int>("_older"))
				.Build();

			var newer = Build.Type("older")
				.WithField(Build.Field<int>("_older").WithVisibility(Visibility.Public))
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
				.WithMethods(Build.Method("One").WithParameters(Build.Parameter<int>("value")))
				.Build();

			ChangesShouldBe(older, newer, typeof(MethodAdded));
		}

		[Fact]
		public void When_a_method_overload_is_removed()
		{
			var older = Build.Type("")
				.WithMethods(Build.Method("One"))
				.WithMethods(Build.Method("One").WithParameters(Build.Parameter<int>("value")))
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

		[Fact]
		public void When_a_ctor_is_added()
		{
			var older = Build.Type("").WithCtors(Build.Ctor()).Build();
			var newer = Build.Type("").WithCtors(Build.Ctor(), Build.Ctor(), Build.Ctor()).Build();

			ChangesShouldBe(older, newer, typeof(CtorAdded), typeof(CtorAdded));
		}

		[Fact]
		public void When_a_ctor_is_removed()
		{
			var older = Build.Type("").WithCtors(Build.Ctor(), Build.Ctor(), Build.Ctor()).Build();
			var newer = Build.Type("").WithCtors(Build.Ctor()).Build();

			ChangesShouldBe(older, newer, typeof(CtorRemoved), typeof(CtorRemoved));
		}

		[Fact]
		public void When_a_ctor_is_changed()
		{
			var older = Build.Type("").WithCtors(Build.Ctor()).Build();
			var newer = Build.Type("").WithCtors(Build.Ctor().WithParameters(Build.Parameter<int>("one"))).Build();

			ChangesShouldBe(older, newer, typeof(CtorArgumentAdded));
		}

		[Fact]
		public void When_a_type_has_a_generic_argument_added()
		{
			var older = Build.Type("").WithGenericArguments(Build.Generic("T")).Build();
			var newer = Build.Type("").WithGenericArguments(Build.Generic("T"), Build.Generic("TVal")).Build();

			ChangesShouldBe(older, newer, typeof(GenericArgumentAdded));
		}

		[Fact]
		public void When_a_type_has_two_generic_arguments_added()
		{
			var older = Build.Type("").Build();
			var newer = Build.Type("").WithGenericArguments(Build.Generic("T"), Build.Generic("TVal")).Build();

			ChangesShouldBe(older, newer, typeof(GenericArgumentAdded), typeof(GenericArgumentAdded));
		}

		[Fact]
		public void When_a_type_has_a_generic_argument_removed()
		{
			var older = Build.Type("").WithGenericArguments(Build.Generic("T"), Build.Generic("TVal")).Build();
			var newer = Build.Type("").WithGenericArguments(Build.Generic("T")).Build();

			ChangesShouldBe(older, newer, typeof(GenericArgumentRemoved));
		}

		[Fact]
		public void When_a_type_has_two_generic_arguments_removed()
		{
			var older = Build.Type("").WithGenericArguments(Build.Generic("T"), Build.Generic("TVal")).Build();
			var newer = Build.Type("").Build();

			ChangesShouldBe(older, newer, typeof(GenericArgumentRemoved), typeof(GenericArgumentRemoved));
		}

		[Fact]
		public void When_a_types_generic_arguments_change_order()
		{
			var older = Build.Type("").WithGenericArguments(Build.Generic("T"), Build.Generic("TVal")).Build();
			var newer = Build.Type("").WithGenericArguments(Build.Generic("TVal"), Build.Generic("T")).Build();

			ChangesShouldBe(older, newer, typeof(GenericArgumentPositionChanged), typeof(GenericArgumentPositionChanged));
		}
	}
}
