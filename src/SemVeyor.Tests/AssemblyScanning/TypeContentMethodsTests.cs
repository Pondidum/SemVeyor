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
		public void The_public_method_is_listed() => Content.Methods.ShouldContain(x => x.Visibility == Visbility.Public);

		[Fact]
		public void The_protected_method_is_listed() => Content.Methods.ShouldContain(x => x.Visibility == Visbility.Protected);

		[Fact]
		public void The_internal_method_is_not_listed() => Content.Methods.ShouldNotContain(x => x.Visibility == Visbility.Internal);

		[Fact]
		public void The_private_method_is_not_listed() => Content.Methods.ShouldNotContain(x => x.Visibility == Visbility.Private);

		public class TestType
		{
			public int Method() { throw new NotSupportedException(); }
			internal int InternalMethod() { throw new NotSupportedException(); }
			protected int ProtectedMethod() { throw new NotSupportedException(); }
			private int PrivateMethod() { throw new NotSupportedException(); }
		}
	}
}