using System.Linq;
using SemVeyor.AssemblyScanning;
using Shouldly;
using Xunit;

namespace SemVeyor.Tests.AssemblyScanning
{
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
