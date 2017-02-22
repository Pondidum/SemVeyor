using System.Linq;
using SemVeyor.AssemblyScanning;

namespace SemVeyor.Tests.Builder
{
	public class CtorDetailsBuilder
	{
		private readonly CtorDetails _ctor;

		public CtorDetailsBuilder()
		{
			_ctor = new CtorDetails
			{
				Arguments = Enumerable.Empty<ArgumentDetails>()
			};
		}

		public CtorDetailsBuilder WithVisibility(Visibility visibility)
		{
			_ctor.Visibility = visibility;
			return this;
		}

		public CtorDetailsBuilder WithArguments(params ArgumentDetails[] arguments)
		{
			_ctor.Arguments = _ctor.Arguments.Concat(arguments).ToArray();
			return this;
		}

		public CtorDetails Build() => _ctor;

		public static implicit operator CtorDetails(CtorDetailsBuilder builder) => builder.Build();
	}
}
