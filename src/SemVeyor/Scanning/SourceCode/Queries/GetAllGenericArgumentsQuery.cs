using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using SemVeyor.Domain;

namespace SemVeyor.Scanning.SourceCode.Queries
{
	public class GetAllGenericArgumentsQuery
	{
		public IEnumerable<GenericArgumentDetails> Execute(INamedTypeSymbol typeSymbol)
		{
			return typeSymbol.TypeParameters.Select((ts, i) => new GenericArgumentDetails
			{
				Name = ts.Name,
				Position = i,
				Constraints = ts.ConstraintTypes.Select(ct => ct.Name)
			});
		}
	}
}
