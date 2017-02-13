using System;
using SemVeyor.AssemblyScanning;

namespace SemVeyor.Tests.Builder
{
	public class FieldDetailsBuilder
	{
		private readonly FieldDetails _field;

		public FieldDetailsBuilder(string name, Type type)
		{
			_field = new FieldDetails
			{
				Type =  type,
				Name = name,
				Visibility = Visibility.Private
			};
		}

		public FieldDetailsBuilder WithVisibility(Visibility visibility)
		{
			_field.Visibility = visibility;
			return this;
		}

		public FieldDetails Build() => _field;

		public static implicit operator FieldDetails(FieldDetailsBuilder builder) => builder.Build();
	}
}
