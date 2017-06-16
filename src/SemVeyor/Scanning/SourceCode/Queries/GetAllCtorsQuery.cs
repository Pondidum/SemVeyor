using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using SemVeyor.Domain;

namespace SemVeyor.Scanning.SourceCode.Queries
{
	public class GetAllCtorsQuery
	{
		public IEnumerable<CtorDetails> Execute(INamedTypeSymbol type)
		{
			return type
				.Constructors
				.Select(c => new CtorDetails
				{
					Name = c.Name,
					Visibility = Helpers.VisibilityFrom(c.DeclaredAccessibility),
					Parameters = c.Parameters.Select((p, index) => new ParameterDetails
					{
						Name = p.Name,
						Position = index,
						Type = new TypeName(p.Type.GetFullMetadataName())
					})
				})
				.Where(MemberDetails.IsExternal);
		}
	}
}
