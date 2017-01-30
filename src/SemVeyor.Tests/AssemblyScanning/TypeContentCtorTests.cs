using System;
using System.Linq;
using SemVeyor.AssemblyScanning;
using Shouldly;
using Xunit;

namespace SemVeyor.Tests.AssemblyScanning
{
	public class TypeContentCtorTests : TypeContentTest<TypeContentCtorTests.TestType>
	{
		[Fact]
		public void There_are_2_constructors() => Content.Constructors.Count().ShouldBe(2);

		[Fact]
		public void The_public_method_is_listed() => Content.Constructors.ShouldContain(x => x.Visibility == Visbility.Public);

		[Fact]
		public void The_protected_method_is_listed() => Content.Constructors.ShouldContain(x => x.Visibility == Visbility.Protected);

		[Fact]
		public void The_internal_method_is_not_listed() => Content.Constructors.ShouldNotContain(x => x.Visibility == Visbility.Internal);

		[Fact]
		public void The_private_method_is_not_listed() => Content.Constructors.ShouldNotContain(x => x.Visibility == Visbility.Private);

		public class TestType
		{
			public TestType() {}
			internal TestType(int arg) {}
			protected TestType(string arg) {}
			private TestType(Guid arg) {}
		}
	}
}
