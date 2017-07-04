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
	public class GetAllCtorsQueryTests : GetAllCtorsQueryTestBase
	{
		protected override IEnumerable<CtorDetails> BuildDetails()
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
				.Single(cd => cd.TypeParameterList == null);

			var model = compilation.GetSemanticModel(classDeclaration.SyntaxTree);

			return new GetAllCtorsQuery().Execute(model.GetDeclaredSymbol(classDeclaration));
		}
	}
}
