using System;
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
		private readonly GetAllCtorsQuery _getConstructors;
		private readonly GetAllMethodsQuery _getMethods;
		private readonly GetAllPropertiesQuery _getProperties;

		public GetTypeQuery()
		{
			_getFields = new GetAllFieldsQuery();
			_getConstructors = new GetAllCtorsQuery();
			_getMethods = new GetAllMethodsQuery();
			_getProperties = new GetAllPropertiesQuery();
		}

		public TypeDetails Execute(SemanticModel model, ClassDeclarationSyntax classDeclaration)
		{
			var typeSymbol = model.GetDeclaredSymbol(classDeclaration);

			return new TypeDetails
			{
				Namespace = typeSymbol.ContainingNamespace.ToDisplayString(),
				Name = typeSymbol.Name + (typeSymbol.TypeParameters.Any() ? "`" + typeSymbol.TypeArguments.Count() : ""),
				Visibility = Helpers.VisibilityFrom(classDeclaration.Modifiers),
				BaseType = typeSymbol.BaseType.Name,
				Interfaces = typeSymbol.Interfaces.Select(i => i.Name),
				Fields = _getFields.Execute(typeSymbol),
				Constructors = _getConstructors.Execute(typeSymbol),
				Methods = _getMethods.Execute(typeSymbol),
				Properties = _getProperties.Execute(typeSymbol)
			};
		}
	}
}
