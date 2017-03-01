using System;
using System.Linq;
using SemVeyor.Domain;

namespace SemVeyor.Tests.Builder
{
	public class PropertyDetailsBuilder
	{
		private readonly PropertyDetails _property;

		public PropertyDetailsBuilder(string name, Type type)
		{
			_property = new PropertyDetails
			{
				Type = type,
				Name = name,
				Parameters = Enumerable.Empty<ParameterDetails>()
			};
		}

		public PropertyDetailsBuilder WithVisibility(Visibility visibility)
		{
			_property.Visibility = visibility;
			return this;
		}

		public PropertyDetailsBuilder WithSetterVisibility(Visibility? visibility)
		{
			_property.SetterVisibility = visibility;
			return this;
		}

		public PropertyDetailsBuilder WithParameters(params ParameterDetails[] parameters)
		{
			_property.Parameters = _property.Parameters.Concat(parameters).ToArray();
			return this;
		}

		public PropertyDetails Build() => _property;

		public static implicit operator PropertyDetails(PropertyDetailsBuilder builder) => builder.Build();
	}
}
