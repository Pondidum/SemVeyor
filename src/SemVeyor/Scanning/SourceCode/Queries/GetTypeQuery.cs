using System;
using System.Collections.Generic;
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

		public GetTypeQuery()
		{
			_getFields = new GetAllFieldsQuery();
			_getConstructors = new GetAllCtorsQuery();
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
				Fields = _getFields.Execute(typeSymbol),
				Constructors = _getConstructors.Execute(typeSymbol)
			};
		}
	}

	public class GetAllCtorsQuery
	{
		public IEnumerable<CtorDetails> Execute(ITypeSymbol type)
		{
			throw new NotImplementedException();
		}
	}
}
