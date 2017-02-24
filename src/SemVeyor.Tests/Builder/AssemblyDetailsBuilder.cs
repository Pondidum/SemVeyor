using System.Linq;
using SemVeyor.Domain;

namespace SemVeyor.Tests.Builder
{
	public class AssemblyDetailsBuilder
	{
		private readonly AssemblyDetails _assembly;

		public AssemblyDetailsBuilder(string name)
		{
			_assembly = new AssemblyDetails
			{
				Name = name,
				Types = Enumerable.Empty<TypeDetails>()
			};
		}

		public AssemblyDetailsBuilder WithTypes(params TypeDetails[] types)
		{
			_assembly.Types = _assembly.Types.Concat(types).ToArray();
			return this;
		}

		public AssemblyDetails Build() => _assembly;

		public static implicit operator AssemblyDetails(AssemblyDetailsBuilder builder) => builder.Build();
	}
}
