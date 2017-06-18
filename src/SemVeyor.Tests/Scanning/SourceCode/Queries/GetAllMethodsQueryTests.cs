using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis;
using SemVeyor.Domain;
using SemVeyor.Scanning.SourceCode.Queries;
using SemVeyor.Tests.TestUtils;

namespace SemVeyor.Tests.Scanning.SourceCode.Queries
{
	public class GetAllMethodsQueryTests : GetAllMethodsQueryTestBase
	{
		protected override IEnumerable<MethodDetails> BuildMethods()
		{
			var compilation = TestCompilationLoader.Get();

			var classDeclaration = compilation
				.SyntaxTrees
				.SelectMany(tree => tree
					.GetRoot()
					.DescendantNodesAndSelf()
					.Where(t => t.IsKind(SyntaxKind.ClassDeclaration)))
				.Cast<ClassDeclarationSyntax>()
				.Where(cd => cd.Identifier.ValueText == nameof(TestType))
				.Where(cd => cd.TypeParameterList == null)
				.Single();

			var model = compilation.GetSemanticModel(classDeclaration.SyntaxTree);

			return new GetAllMethodsQuery().Execute(model.GetDeclaredSymbol(classDeclaration));
		}
	}
}