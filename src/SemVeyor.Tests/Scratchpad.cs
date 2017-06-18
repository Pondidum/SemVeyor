using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using SemVeyor.Domain;
using SemVeyor.Infrastructure;
using SemVeyor.Scanning;
using SemVeyor.Scanning.SourceCode;
using SemVeyor.Scanning.SourceCode.Queries;
using SemVeyor.Storage;
using SemVeyor.Tests.Scanning.SourceCode.Queries;
using SemVeyor.Tests.TestUtils;
using Shouldly;
using Xunit;
using Xunit.Abstractions;

namespace SemVeyor.Tests
{
	public class Scratchpad
	{
		private readonly ITestOutputHelper _output;

		public Scratchpad(ITestOutputHelper output)
		{
			_output = output;
		}

		[Fact]
		public async Task When_testing_something()
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

			var type = model.GetDeclaredSymbol(classDeclaration);

			var methods = type
				.GetMembers()
				.OfType<IMethodSymbol>()
				.Where(t => t.MethodKind == MethodKind.Ordinary)
				.Where(t => t.IsExtern == false);

			foreach (var ms in methods)
			{
				_output.WriteLine(string.Join(" ", ms.Name, ms.IsCheckedBuiltin, ms.IsDefinition));
			}

		}
	}
}
