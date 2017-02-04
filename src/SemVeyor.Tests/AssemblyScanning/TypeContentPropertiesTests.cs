using System;
using System.Linq;
using SemVeyor.AssemblyScanning;
using Shouldly;
using Xunit;

namespace SemVeyor.Tests.AssemblyScanning
{
	public class TypeContentPropertiesTests : TypeContentTestBase<TypeContentPropertiesTests.TestType>
	{
		[Fact]
		public void There_are_7_properties() => Content.Properties.Count().ShouldBe(7);

		[Fact]
		public void The_public_property_is_listed() => Content.Properties.ShouldContain( x => x.Name == nameof(TestType.Property));

		[Fact]
		public void The_readonly_property_is_listed() => Content.Properties.ShouldContain(x => x.Name == nameof(TestType.ReadonlyProperty));

		[Fact]
		public void The_private_setter_property_is_listed() => Content.Properties.ShouldContain(x => x.Name == nameof(TestType.PrivateSetProperty));

		[Fact]
		public void The_internal_setter_property_is_listed() => Content.Properties.ShouldContain(x => x.Name == nameof(TestType.InternalSetProperty));

		[Fact]
		public void The_protected_setter_property_is_listed() => Content.Properties.ShouldContain(x => x.Name == nameof(TestType.ProtectedSetProperty));

		[Fact]
		public void The_protected_property_is_listed() => Content.Properties.ShouldContain(x => x.Name == "ProtectedProperty");

		[Fact]
		public void The_internal_property_is_not_listed() => Content.Properties.ShouldNotContain(x => x.Name == "InternalProperty");

		[Fact]
		public void The_private_property_is_not_listed() => Content.Properties.ShouldNotContain(x => x.Name == "PrivateProperty");

		[Fact]
		public void The_property_name_is_populated() => Property.Name.ShouldBe(nameof(TestType.InternalSetProperty));

		[Fact]
		public void The_type_is_populated() => Property.Type.ShouldBe(typeof(int));

		[Fact]
		public void The_setter_visibility_is_populated() => Property.SetterVisibility.ShouldBe(Visibility.Internal);

		[Fact]
		public void When_there_is_no_setter_its_visibility_is_null() => ReadOnlyProperty.SetterVisibility.ShouldBeNull();

		[Fact]
		public void An_indexed_property_argument_is_populated() => IndexedProperty.Arguments.Count().ShouldBe(1);

		[Fact]
		public void An_indexed_property_name_is_populated() => IndexedProperty.Name.ShouldBe("Item");

		private PropertyDetails Property => Content.Properties.FirstOrDefault(p => p.Name == nameof(TestType.InternalSetProperty));
		private PropertyDetails ReadOnlyProperty => Content.Properties.FirstOrDefault(p => p.Name == nameof(TestType.ReadonlyProperty));
		private PropertyDetails IndexedProperty => Content.Properties.FirstOrDefault(p => p.Type== typeof(string));

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

			public string this[int index]
			{
				get { throw new Exception(); }
				set { throw new Exception(); }
			}
		}
	}
}
