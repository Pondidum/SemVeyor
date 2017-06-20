using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using SemVeyor.Domain;

namespace SemVeyor.Scanning.SourceCode.Queries
{
	public class GetAllPropertiesQuery
	{
		public IEnumerable<PropertyDetails> Execute(INamedTypeSymbol typeSymbol)
		{
			return typeSymbol
				.GetMembers()
				.OfType<IPropertySymbol>()
				.Where(p => p.IsStatic == false)
				.Select(pd => new PropertyDetails
				{
					Name = pd.MetadataName,
					Type = new TypeName(pd.Type.GetFullMetadataName()),
					Parameters = pd.Parameters.Select(ParameterDetailsBuilder.Build),
					Visibility = Helpers.VisibilityFrom(pd.DeclaredAccessibility),
					SetterVisibility = pd.SetMethod != null
						? Helpers.VisibilityFrom(pd.SetMethod.DeclaredAccessibility)
						: (Visibility?)null
				})
				.Where(MemberDetails.IsExternal);
		}
	}
}
