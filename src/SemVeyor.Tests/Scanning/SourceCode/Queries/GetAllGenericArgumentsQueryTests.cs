using System;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using SemVeyor.Domain;
using SemVeyor.Scanning.SourceCode.Queries;
using SemVeyor.Tests.TestUtils;

namespace SemVeyor.Tests.Scanning.SourceCode.Queries
{
	public class GetAllGenericArgumentsQueryTests : GetAllGenericArgumentsQueryTestBase
	{
		protected override IEnumerable<GenericArgumentDetails> BuildGenerics()
		{
			var compilation = TestCompilationLoader.Get();

			var classDeclaration = compilation
				.SyntaxTrees
				.SelectMany(tree => tree
					.GetRoot()
					.DescendantNodesAndSelf()
					.Where(t => t.IsKind(SyntaxKind.ClassDeclaration)))
				.Cast<ClassDeclarationSyntax>()
				.Single(cd => cd.Identifier.ValueText == nameof(GenericType<Exception, int>));

			var model = compilation.GetSemanticModel(classDeclaration.SyntaxTree);

			return new GetAllGenericArgumentsQuery().Execute(model.GetDeclaredSymbol(classDeclaration));
		}
	}
}
