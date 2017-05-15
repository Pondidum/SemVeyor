using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
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

	public class Helpers
	{
		public static Visibility VisibilityFrom(SyntaxTokenList modifiers)
		{
			if (modifiers.Any(SyntaxKind.PublicKeyword))
				return Visibility.Public;

			if (modifiers.Any(SyntaxKind.ProtectedKeyword))
				return Visibility.Protected;

			if (modifiers.Any(SyntaxKind.InternalKeyword))
				return Visibility.Internal;

			return Visibility.Private;
		}

		public static Visibility VisibilityFrom(Accessibility accessibility)
		{
			if (accessibility == Accessibility.Public)
				return Visibility.Public;

			if (accessibility == Accessibility.Protected)
				return Visibility.Protected;

			if (accessibility == Accessibility.Internal)
				return Visibility.Internal;

			return Visibility.Private;
		}
	}

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
					Visibility = Helpers.VisibilityFrom(fs.DeclaredAccessibility)
				})
				.Where(MemberDetails.IsExternal);
		}
	}
}