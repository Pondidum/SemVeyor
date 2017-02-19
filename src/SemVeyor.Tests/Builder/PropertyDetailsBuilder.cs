using System;
using System.Linq;
using SemVeyor.AssemblyScanning;

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
				Arguments = Enumerable.Empty<ArgumentDetails>()
			};
		}

		public PropertyDetailsBuilder WithVisibility(Visibility visibility)
		{
			_property.Visibility = visibility;
			return this;
		}

		public PropertyDetailsBuilder WithSetterVisibility(Visibility visibility)
		{
			_property.SetterVisibility = visibility;
			return this;
		}

		public PropertyDetailsBuilder WithArguments(params ArgumentDetails[] arguments)
		{
			_property.Arguments = _property.Arguments.Concat(arguments).ToArray();
			return this;
		}

		public PropertyDetails Build() => _property;

		public static implicit operator PropertyDetails(PropertyDetailsBuilder builder) => builder.Build();
	}
}
