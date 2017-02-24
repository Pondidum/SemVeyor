using System.Linq;
using SemVeyor.Domain;

namespace SemVeyor.Tests.Builder
{
	public class GenericArgumentDetailsBuilder
	{
		private readonly GenericArgumentDetails _arg;

		public GenericArgumentDetailsBuilder(string name)
		{
			_arg = new GenericArgumentDetails
			{
				Name = name,
				Position = 0,
				Constraints = Enumerable.Empty<string>()
			};
		}

		public GenericArgumentDetailsBuilder WithPosition(int position)
		{
			_arg.Position = position;
			return this;
		}

		public GenericArgumentDetailsBuilder WithConstraints(params string[] constraints)
		{
			_arg.Constraints = _arg.Constraints.Concat(constraints).ToArray();
			return this;
		}

		public GenericArgumentDetails Build() => _arg;

		public static implicit operator GenericArgumentDetails(GenericArgumentDetailsBuilder builder) => builder.Build();
	}
}
