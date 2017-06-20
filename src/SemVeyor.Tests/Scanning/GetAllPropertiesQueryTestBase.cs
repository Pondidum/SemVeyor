using System;
using System.Collections.Generic;
using System.Linq;
using SemVeyor.Domain;
using SemVeyor.Tests.TestUtils;
using Shouldly;
using Xunit;

namespace SemVeyor.Tests.Scanning
{
	public abstract class GetAllPropertiesQueryTestBase
	{
		private readonly IEnumerable<PropertyDetails> _properties;

		public GetAllPropertiesQueryTestBase()
		{
			_properties = BuildProperties();
		}

		protected abstract IEnumerable<PropertyDetails> BuildProperties();

		[Fact]
		public void There_are_8_properties() => _properties.Count().ShouldBe(8);

		[Fact]
		public void The_public_property_is_listed() => _properties.ShouldContain( x => x.Name == nameof(TestType.Property));

		[Fact]
		public void The_readonly_property_is_listed() => _properties.ShouldContain(x => x.Name == nameof(TestType.ReadonlyProperty));

		[Fact]
		public void The_private_setter_property_is_listed() => _properties.ShouldContain(x => x.Name == nameof(TestType.PrivateSetProperty));

		[Fact]
		public void The_internal_setter_property_is_listed() => _properties.ShouldContain(x => x.Name == nameof(TestType.InternalSetProperty));

		[Fact]
		public void The_protected_setter_property_is_listed() => _properties.ShouldContain(x => x.Name == nameof(TestType.ProtectedSetProperty));

		[Fact]
		public void The_protected_property_is_listed() => _properties.ShouldContain(x => x.Name == "ProtectedProperty");

		[Fact]
		public void The_internal_property_is_not_listed() => _properties.ShouldNotContain(x => x.Name == "InternalProperty");

		[Fact]
		public void The_private_property_is_not_listed() => _properties.ShouldNotContain(x => x.Name == "PrivateProperty");

		[Fact]
		public void The_property_name_is_populated() => Property.Name.ShouldBe(nameof(TestType.InternalSetProperty));

		[Fact]
		public void The_property_type_is_populated() => Property.Type.ShouldBe(typeof(int));

		[Fact]
		public void The_property_setter_visibility_is_populated() => Property.SetterVisibility.ShouldBe(Visibility.Internal);

		[Fact]
		public void The_propety_with_no_setters_visibility_is_null() => ReadOnlyProperty.SetterVisibility.ShouldBeNull();

		[Fact]
		public void An_indexed_property_argument_is_populated() => IndexedProperty.Parameters.Count().ShouldBe(1);

		[Fact]
		public void A_multi_indexed_property_arguments_are_populated()
		{
			var parameters = MultiIndexedProperty.Parameters.ToArray();
			
			parameters.ShouldSatisfyAllConditions(
				() => parameters.Length.ShouldBe(2),
				() => parameters[0].Name.ShouldBe("x"),
				() => parameters[0].Type.ShouldBe(new TypeName(typeof(int).FullName)),
				() => parameters[0].Position.ShouldBe(0),
				() => parameters[1].Name.ShouldBe("y"),
				() => parameters[1].Type.ShouldBe(new TypeName(typeof(int).FullName)),
				() => parameters[1].Position.ShouldBe(1)
			);
		}

		[Fact]
		public void An_indexed_property_name_is_populated() => IndexedProperty.Name.ShouldBe("Item");

		private PropertyDetails Property => _properties.FirstOrDefault(p => p.Name == nameof(TestType.InternalSetProperty));
		private PropertyDetails ReadOnlyProperty => _properties.FirstOrDefault(p => p.Name == nameof(TestType.ReadonlyProperty));
		private PropertyDetails IndexedProperty => _properties.FirstOrDefault(p => p.Type == typeof(string));
		private PropertyDetails MultiIndexedProperty => _properties.FirstOrDefault(p => p.Type == typeof(Guid));
		
	}
}