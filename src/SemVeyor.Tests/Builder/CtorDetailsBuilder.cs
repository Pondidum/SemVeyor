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
				Name = "ctor",
				Parameters = Enumerable.Empty<ParameterDetails>()
			};
		}

		public CtorDetailsBuilder WithVisibility(Visibility visibility)
		{
			_ctor.Visibility = visibility;
			return this;
		}

		public CtorDetailsBuilder WithArguments(params ParameterDetails[] parameters)
		{
			_ctor.Parameters = _ctor.Parameters.Concat(parameters).ToArray();
			return this;
		}

		public CtorDetails Build() => _ctor;

		public static implicit operator CtorDetails(CtorDetailsBuilder builder) => builder.Build();
	}
}
