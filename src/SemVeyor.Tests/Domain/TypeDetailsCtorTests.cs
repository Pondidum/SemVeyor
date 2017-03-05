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
		public void There_are_2_fields() => Details.Fields.Count().ShouldBe(2);

		[Fact]
		public void There_are_7_properties() => Details.Properties.Count().ShouldBe(7);



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




		private MethodDetails PublicMethod => Details.Methods.ByVisibility(Visibility.Public);
		private MethodDetails GenericMethod => Details.Methods.Single(m => m.Name == TestType.GenericMethodName);

		private static int MethodsOnObject() => typeof(object)
			.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly)
			.Count();
	}
}
