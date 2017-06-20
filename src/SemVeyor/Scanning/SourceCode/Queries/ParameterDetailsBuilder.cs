using Microsoft.CodeAnalysis;
using SemVeyor.Domain;

namespace SemVeyor.Scanning.SourceCode.Queries
{
	public class ParameterDetailsBuilder
	{
		public static ParameterDetails Build(IParameterSymbol parameter)
		{
			return new ParameterDetails
			{
				Name = parameter.Name,
				Type = new TypeName(parameter.Type.GetFullMetadataName()),
				Position = parameter.Ordinal
			};
		}
	}
}
