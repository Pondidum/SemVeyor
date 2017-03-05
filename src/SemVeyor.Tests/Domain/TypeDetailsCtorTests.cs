using System;
using System.Linq;
using System.Reflection;
using SemVeyor.Domain;
using SemVeyor.Tests.TestUtils;
using Shouldly;
using Xunit;

namespace SemVeyor.Tests.Domain
{
	public class TypeDetailsCtorTests
	{
		protected Type InputType { get; }
		protected TypeDetails Details { get; }

		public TypeDetailsCtorTests()
		{
			InputType = typeof(TestType);
			Details = TypeDetails.From(typeof(TestType));
		}

		[Fact]
		public void The_name_is_populated() => Details.Name.ShouldBe(InputType.Name);

		[Fact]
		public void The_namespace_is_populated() => Details.Namespace.ShouldBe(InputType.Namespace);



		[Fact]
		public void There_are_2_constructors() => Details.Constructors.Count().ShouldBe(2);

		[Fact]
		public void The_public_ctor_is_listed() => Details.Constructors.ShouldContain(x => x.Visibility == Visibility.Public);

		[Fact]
		public void The_protected_ctor_is_listed() => Details.Constructors.ShouldContain(x => x.Visibility == Visibility.Protected);

		[Fact]
		public void The_internal_ctor_is_not_listed() => Details.Constructors.ShouldNotContain(x => x.Visibility == Visibility.Internal);

		[Fact]
		public void The_private_ctor_is_not_listed() => Details.Constructors.ShouldNotContain(x => x.Visibility == Visibility.Private);

		[Fact]
		public void The_ctor_name_is_populated() => Details.Constructors.ByVisibility(Visibility.Protected).Name.ShouldBe(".ctor");

		[Fact]
		public void The_ctor_arguments_are_populated() => Details.Constructors.ByVisibility(Visibility.Protected).Parameters.Count().ShouldBe(1);



		[Fact]
		public void There_are_2_fields() => Details.Fields.Count().ShouldBe(2);

		[Fact]
		public void The_public_field_is_listed() => Details.Fields.ShouldContain(x => x.Visibility == Visibility.Public);

		[Fact]
		public void The_protected_field_is_listed() => Details.Fields.ShouldContain(x => x.Visibility == Visibility.Protected);

		[Fact]
		public void The_internal_field_is_not_listed() => Details.Fields.ShouldNotContain(x => x.Visibility == Visibility.Internal);

		[Fact]
		public void The_private_field_is_not_listed() => Details.Fields.ShouldNotContain(x => x.Visibility == Visibility.Private);

		[Fact]
		public void The_field_name_is_populated() => PublicField.Name.ShouldBe(nameof(TestType.PublicField));

		[Fact]
		public void The_field_type_is_populated() => PublicField.Type.ShouldBe(typeof(int));



		[Fact]
		public void There_are_7_properties() => Details.Properties.Count().ShouldBe(7);

		[Fact]
		public void The_public_property_is_listed() => Details.Properties.ShouldContain( x => x.Name == nameof(TestType.Property));

		[Fact]
		public void The_readonly_property_is_listed() => Details.Properties.ShouldContain(x => x.Name == nameof(TestType.ReadonlyProperty));

		[Fact]
		public void The_private_setter_property_is_listed() => Details.Properties.ShouldContain(x => x.Name == nameof(TestType.PrivateSetProperty));

		[Fact]
		public void The_internal_setter_property_is_listed() => Details.Properties.ShouldContain(x => x.Name == nameof(TestType.InternalSetProperty));

		[Fact]
		public void The_protected_setter_property_is_listed() => Details.Properties.ShouldContain(x => x.Name == nameof(TestType.ProtectedSetProperty));

		[Fact]
		public void The_protected_property_is_listed() => Details.Properties.ShouldContain(x => x.Name == "ProtectedProperty");

		[Fact]
		public void The_internal_property_is_not_listed() => Details.Properties.ShouldNotContain(x => x.Name == "InternalProperty");

		[Fact]
		public void The_private_property_is_not_listed() => Details.Properties.ShouldNotContain(x => x.Name == "PrivateProperty");

		[Fact]
		public void The_property_name_is_populated() => Property.Name.ShouldBe(nameof(TestType.InternalSetProperty));

		[Fact]
		public void The_property_type_is_populated() => Property.Type.ShouldBe(typeof(int));

		[Fact]
		public void The_property_setter_visibility_is_populated() => Property.SetterVisibility.ShouldBe(Visibility.Internal);

		[Fact]
		public void The_propety_with_no_setters_visibility_is_null() => ReadOnlyProperty.SetterVisibility.ShouldBeNull();

		[Fact]
		public void An_indexed_property_argument_is_populated() => IndexedProperty.Parameters.Count().ShouldBe(1);

		[Fact]
		public void An_indexed_property_name_is_populated() => IndexedProperty.Name.ShouldBe("Item");



		[Fact]
		public void There_are_3_methods() => Details.Methods.Count().ShouldBe(MethodsOnObject() + 3);

		[Fact]
		public void The_public_method_is_listed() => Details.Methods.ShouldContain(x => x.Visibility == Visibility.Public);

		[Fact]
		public void The_protected_method_is_listed() => Details.Methods.ShouldContain(x => x.Visibility == Visibility.Protected);

		[Fact]
		public void The_internal_method_is_not_listed() => Details.Methods.ShouldNotContain(x => x.Visibility == Visibility.Internal);

		[Fact]
		public void The_private_method_is_not_listed() => Details.Methods.ShouldNotContain(x => x.Visibility == Visibility.Private);

		[Fact]
		public void The_method_name_is_populated() => PublicMethod.Name.ShouldBe(nameof(TestType.Method));

		[Fact]
		public void The_return_type_is_populated() => PublicMethod.Type.ShouldBe(typeof(int));

		[Fact]
		public void The_method_arguments_are_populated() => PublicMethod.Parameters.Count().ShouldBe(3);

		[Fact]
		public void The_method_generic_arguments_are_populated() => GenericMethod.GenericArguments.Count().ShouldBe(1);



		[Fact]
		public void The_base_type_is_populated() => Details.BaseType.ShouldBe(nameof(ParentType));

		[Fact]
		public void The_interfaces_are_populated() => Details.Interfaces.ShouldBe(new[]
		{
			nameof(ITestInterfaceOne),
			nameof(ITestInterfaceTwo)
		});



		private FieldDetails PublicField => Details.Fields.ByVisibility(Visibility.Public);
		private MethodDetails PublicMethod => Details.Methods.ByVisibility(Visibility.Public);
		private PropertyDetails Property => Details.Properties.FirstOrDefault(p => p.Name == nameof(TestType.InternalSetProperty));
		private PropertyDetails ReadOnlyProperty => Details.Properties.FirstOrDefault(p => p.Name == nameof(TestType.ReadonlyProperty));
		private PropertyDetails IndexedProperty => Details.Properties.FirstOrDefault(p => p.Type== typeof(string));
		private MethodDetails GenericMethod => Details.Methods.Single(m => m.Name == TestType.GenericMethodName);

		private static int MethodsOnObject() => typeof(object)
			.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly)
			.Count();
	}
}
