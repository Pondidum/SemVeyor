using System;
using System.Linq;
using SemVeyor.AssemblyScanning;
using Shouldly;
using Xunit;

namespace SemVeyor.Tests.AssemblyScanning
{
	public class TypeContentCtorTests : TypeContentTestBase<TypeContentCtorTests.TestType>
	{
		[Fact]
		public void There_are_2_constructors() => Content.Constructors.Count().ShouldBe(2);

		[Fact]
		public void The_public_ctor_is_listed() => Content.Constructors.ShouldContain(x => x.Visibility == Visibility.Public);

		[Fact]
		public void The_protected_ctor_is_listed() => Content.Constructors.ShouldContain(x => x.Visibility == Visibility.Protected);

		[Fact]
		public void The_internal_ctor_is_not_listed() => Content.Constructors.ShouldNotContain(x => x.Visibility == Visibility.Internal);

		[Fact]
		public void The_private_ctor_is_not_listed() => Content.Constructors.ShouldNotContain(x => x.Visibility == Visibility.Private);

		[Fact]
		public void The_ctor_name_is_populated() => Content.Constructors.ByVisibility(Visibility.Protected).Name.ShouldBe(".ctor");

		[Fact]
		public void The_arguments_are_populated() => Content.Constructors.ByVisibility(Visibility.Protected).Arguments.Count().ShouldBe(1);

		public class TestType
		{
			public TestType() {}
			internal TestType(int arg) {}
			protected TestType(string arg) {}
			private TestType(Guid arg) {}
		}
	}
}
