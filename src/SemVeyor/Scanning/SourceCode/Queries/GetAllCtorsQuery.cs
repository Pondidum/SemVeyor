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
					Visibility = Helpers.VisibilityFrom((Accessibility)c.DeclaredAccessibility)
				})
				.Where(MemberDetails.IsExternal);
		}
	}
}
