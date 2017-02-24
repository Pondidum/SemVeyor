using System;
using SemVeyor.AssemblyScanning;

namespace SemVeyor.Tests.Builder
{
	public class ParameterDetailsBuilder
	{
		private readonly ParameterDetails _parameter;

		public ParameterDetailsBuilder(string name, Type type)
		{
			_parameter = new ParameterDetails
			{
				Name = name,
				Type =  type
			};
		}

		public ParameterDetailsBuilder WithPosition(int position)
		{
			_parameter.Position = position;
			return this;
		}

		public ParameterDetails Build() => _parameter;

		public static implicit operator ParameterDetails(ParameterDetailsBuilder builder) => builder.Build();
	}
}
