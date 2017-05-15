using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using SemVeyor.Domain;

namespace SemVeyor.Scanning.SourceCode.Queries
{
	public class GetAllTypesQuery
	{
		private readonly GetTypeQuery _getTypeQuery;

		public GetAllTypesQuery(GetTypeQuery getTypeQuery)
		{
			_getTypeQuery = getTypeQuery;
		}

		public IEnumerable<TypeDetails> Execute(Compilation compilation)
		{
			return compilation
				.SyntaxTrees
				.SelectMany(tree => tree.GetRoot().DescendantNodesAndSelf().Where(t => t.IsKind(SyntaxKind.ClassDeclaration)))
				.Cast<ClassDeclarationSyntax>()
				.Select(classDeclaration => _getTypeQuery.Execute(compilation.GetSemanticModel(classDeclaration.SyntaxTree), classDeclaration))
				.Where(MemberDetails.IsExternal)
				.ToArray();
		}
	}
}