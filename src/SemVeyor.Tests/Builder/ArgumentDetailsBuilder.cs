using System;
using SemVeyor.AssemblyScanning;

namespace SemVeyor.Tests.Builder
{
	public class ArgumentDetailsBuilder
	{
		private readonly ArgumentDetails _argument;

		public ArgumentDetailsBuilder(string name, Type type)
		{
			_argument = new ArgumentDetails
			{
				Name = name,
				Type =  type
			};
		}

		public ArgumentDetailsBuilder WithPosition(int position)
		{
			_argument.Position = position;
			return this;
		}

		public ArgumentDetails Build() => _argument;

		public static implicit operator ArgumentDetails(ArgumentDetailsBuilder builder) => builder.Build();
	}
}
