using System;
using System.Collections.Generic;
using Shouldly;
using System.Linq;
using System.Reflection;
using SemVeyor.AssemblyScanning;
using Xunit;

namespace SemVeyor.Tests.AssemblyScanning
{
	public class TypeContentMethodsTests : TypeContentTest<TypeContentMethodsTests.TestType>
	{
		private static int MethodsOnObject() => typeof(object)
			.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly)
			.Count();

		[Fact]
		public void There_are_2_methods() => Content.Methods.Count().ShouldBe(MethodsOnObject() + 2);
		[Fact]
		public void The_public_method_is_listed() => Content.Methods.ShouldContain(x => x.Visibility == Visibility.Public);

		[Fact]
		public void The_protected_method_is_listed() => Content.Methods.ShouldContain(x => x.Visibility == Visibility.Protected);

		[Fact]
		public void The_internal_method_is_not_listed() => Content.Methods.ShouldNotContain(x => x.Visibility == Visibility.Internal);

		[Fact]
		public void The_private_method_is_not_listed() => Content.Methods.ShouldNotContain(x => x.Visibility == Visibility.Private);

		[Fact]
		public void The_method_name_is_populated() => PublicMethod.Name.ShouldBe(nameof(TestType.Method));

		[Fact]
		public void The_return_type_is_populated() => PublicMethod.Type.ShouldBe(typeof(int));

		[Fact]
		public void The_arguments_are_populated() => PublicMethod.Arguments.Count().ShouldBe(3);

		private MethodDetails PublicMethod => Content.Methods.ByVisibility(Visibility.Public);

		public class TestType
		{
			public int Method(string first, int second, Action<int> third) { throw new NotSupportedException(); }
			internal int InternalMethod() { throw new NotSupportedException(); }
			protected int ProtectedMethod() { throw new NotSupportedException(); }
			private int PrivateMethod() { throw new NotSupportedException(); }
		}
	}
}