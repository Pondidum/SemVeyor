using System;
using System.Collections.Generic;
using Shouldly;
using System.Linq;
using System.Reflection;
using Xunit;

namespace SemVeyor.Tests.AssemblyScanning
{
	public class TypeContentMethodsTests : TypeContentTest<TypeContentMethodsTests.TestType>
	{
		private static IEnumerable<string> MethodsOnObject() => typeof(object)
			.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly)
			.Select(m => m.Name);

		[Fact]
		public void There_are_2_methods()
		{
			var expectedMethods = MethodsOnObject()
				.Concat(new[] { nameof(TestType.Method), "ProtectedMethod" });

			Content.Methods.ShouldBe(expectedMethods, ignoreOrder: true);
		}

		[Fact]
		public void The_public_method_is_listed() => Content.Methods.ShouldContain(nameof(TestType.Method));

		[Fact]
		public void The_protected_method_is_listed() => Content.Methods.ShouldContain("ProtectedMethod");

		[Fact]
		public void The_internal_method_is_not_listed() => Content.Methods.ShouldNotContain("InternalMethod");

		[Fact]
		public void The_private_method_is_not_listed() => Content.Methods.ShouldNotContain("PrivateMethod");

		public class TestType
		{
			public int Method() { throw new NotSupportedException(); }
			internal int InternalMethod() { throw new NotSupportedException(); }
			protected int ProtectedMethod() { throw new NotSupportedException(); }
			private int PrivateMethod() { throw new NotSupportedException(); }
		}
	}
}