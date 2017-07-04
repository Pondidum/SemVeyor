using System.Linq;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis;
using SemVeyor.Domain;
using SemVeyor.Scanning.SourceCode.Queries;
using SemVeyor.Tests.TestUtils;

namespace SemVeyor.Tests.Scanning.SourceCode.Queries
{
	public class GetTypeQueryTests : GetTypeQueryTestBase
	{
		protected override TypeDetails BuildDetails()
		{
			var compilation = TestCompilationLoader.Get();

			var classes = compilation
				.SyntaxTrees
				.SelectMany(tree => tree
					.GetRoot()
					.DescendantNodesAndSelf()
					.Where(t => t.IsKind(SyntaxKind.ClassDeclaration)))
				.Cast<ClassDeclarationSyntax>()
				.Where(cd => cd.Identifier.ValueText == nameof(TestType))
				.Where(cd => cd.TypeParameterList == null)
				.ToArray();

			var model = compilation.GetSemanticModel(classes.Single().SyntaxTree);

			return new GetTypeQuery().Execute(model, classes.Single());
		}
	}
}