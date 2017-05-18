using System.Linq;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis;
using SemVeyor.Domain;

namespace SemVeyor.Scanning.SourceCode.Queries
{
	public class GetTypeQuery
	{
		private readonly GetAllFieldsQuery _getFields;

		public GetTypeQuery()
		{
			_getFields = new GetAllFieldsQuery();
		}

		public TypeDetails Execute(SemanticModel model, ClassDeclarationSyntax classDeclaration)
		{
			var typeSymbol = model.GetDeclaredSymbol(classDeclaration);

			return new TypeDetails
			{
				Namespace = typeSymbol.ContainingNamespace.ToDisplayString(),
				Name = typeSymbol.Name,
				Visibility = Helpers.VisibilityFrom(classDeclaration.Modifiers),
				BaseType = typeSymbol.BaseType.Name,
				Interfaces = typeSymbol.Interfaces.Select(i => i.Name),
				Fields = _getFields.Execute(typeSymbol)
			};
		}
	}
}