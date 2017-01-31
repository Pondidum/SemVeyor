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
		public void The_public_ctor_is_listed() => Content.Constructors.ShouldContain(x => x.Visibility == Visibility.Public);

		[Fact]
		public void The_protected_ctor_is_listed() => Content.Constructors.ShouldContain(x => x.Visibility == Visibility.Protected);

		[Fact]
		public void The_internal_ctor_is_not_listed() => Content.Constructors.ShouldNotContain(x => x.Visibility == Visibility.Internal);

		[Fact]
		public void The_private_ctor_is_not_listed() => Content.Constructors.ShouldNotContain(x => x.Visibility == Visibility.Private);

		public class TestType
		{
			public TestType() {}
			internal TestType(int arg) {}
			protected TestType(string arg) {}
			private TestType(Guid arg) {}
		}
	}

	public class TypeContentFieldTests : TypeContentTest<TypeContentFieldTests.TestType>
	{
		[Fact]
		public void There_are_2_fields() => Content.Fields.Count().ShouldBe(2);

		[Fact]
		public void The_public_field_is_listed() => Content.Fields.ShouldContain(x => x.Visibility == Visibility.Public);

		[Fact]
		public void The_protected_field_is_listed() => Content.Fields.ShouldContain(x => x.Visibility == Visibility.Protected);

		[Fact]
		public void The_internal_field_is_not_listed() => Content.Fields.ShouldNotContain(x => x.Visibility == Visibility.Internal);

		[Fact]
		public void The_private_field_is_not_listed() => Content.Fields.ShouldNotContain(x => x.Visibility == Visibility.Private);

		public class TestType
		{
			public int PublicField;
			internal int InternalField;
			protected int ProtectedField;
			private int PrivateField;
		}
	}
}
