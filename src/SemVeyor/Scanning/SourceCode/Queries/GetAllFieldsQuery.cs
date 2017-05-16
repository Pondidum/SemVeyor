using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using SemVeyor.Domain;

namespace SemVeyor.Scanning.SourceCode.Queries
{
	public class GetAllFieldsQuery
	{
		public IEnumerable<FieldDetails> Execute(INamedTypeSymbol typeSymbol)
		{
			return typeSymbol
				.GetMembers()
				.OfType<IFieldSymbol>()
				.Where(fs => fs.IsImplicitlyDeclared == false)
				.Select(fs => new FieldDetails
				{
					Name = fs.Name,
					Visibility = Helpers.VisibilityFrom(fs.DeclaredAccessibility),
					Type = new TypeName(fs.Type.Name)
				})
				.Where(MemberDetails.IsExternal);
		}
	}
}
