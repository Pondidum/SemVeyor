using System;
using System.Collections.Generic;
using Shouldly;
using System.Linq;
using System.Reflection;
using SemVeyor.AssemblyScanning;
using Xunit;

namespace SemVeyor.Tests.AssemblyScanning
{
	public class TypeDetailsMethodsTests : TypeDetailsTestBase<TypeDetailsMethodsTests.TestType>
	{
		private static int MethodsOnObject() => typeof(object)
			.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly)
			.Count();

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
		public void The_arguments_are_populated() => PublicMethod.Parameters.Count().ShouldBe(3);

		[Fact]
		public void The_generic_arguments_are_populated() => GenericMethod.GenericArguments.Count().ShouldBe(1);

		private MethodDetails PublicMethod => Details.Methods.ByVisibility(Visibility.Public);
		private MethodDetails GenericMethod => Details.Methods.Single(m => m.Name == TestType.GenericMethodName);

		public class TestType
		{
			public int Method(string first, int second, Action<int> third) { throw new NotSupportedException(); }
			internal int InternalMethod() { throw new NotSupportedException(); }
			protected int ProtectedMethod() { throw new NotSupportedException(); }
			private int PrivateMethod() { throw new NotSupportedException(); }

			protected T GenericMethod<T>(T test, Action<T> action) { throw new NotSupportedException(); }

			public static string GenericMethodName => nameof(GenericMethod);
		}
	}
}
