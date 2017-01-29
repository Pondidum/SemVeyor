using System.Linq;
using System.Reflection;
using Shouldly;
using Xunit;
using Xunit.Abstractions;

namespace SemVeyor.Tests.AssemblyScanning
{
	public class TypeContentPropertiesTests : TypeContentTest<TypeContentPropertiesTests.TestType>
	{
		[Fact]
		public void There_are_6_properties() => Content.Properties.Count().ShouldBe(6);

		[Fact]
		public void The_public_property_is_listed() => Content.Properties.ShouldContain(nameof(TestType.Property));

		[Fact]
		public void The_readonly_property_is_listed() => Content.Properties.ShouldContain(nameof(TestType.ReadonlyProperty));

		[Fact]
		public void The_private_setter_property_is_listed() => Content.Properties.ShouldContain(nameof(TestType.PrivateSetProperty));

		[Fact]
		public void The_internal_setter_property_is_listed() => Content.Properties.ShouldContain(nameof(TestType.InternalSetProperty));

		[Fact]
		public void The_protected_setter_property_is_listed() => Content.Properties.ShouldContain(nameof(TestType.ProtectedSetProperty));

		[Fact]
		public void The_protected_property_is_listed() => Content.Properties.ShouldContain("ProtectedProperty");

		[Fact]
		public void The_internal_property_is_not_listed() => Content.Properties.ShouldNotContain("InternalProperty");

		[Fact]
		public void The_private_property_is_not_listed() => Content.Properties.ShouldNotContain("PrivateProperty");

		public class TestType
		{
			public int Property { get; set; }
			public int ReadonlyProperty { get; }

			public int PrivateSetProperty { get; private set; }
			public int InternalSetProperty { get; internal set; }
			public int ProtectedSetProperty { get; protected set; }

			internal int InternalProperty { get; set; }
			protected int ProtectedProperty { get; set; }
			private int PrivateProperty { get; set; }
		}
	}
}
