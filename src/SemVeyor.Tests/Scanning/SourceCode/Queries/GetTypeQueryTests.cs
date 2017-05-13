using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.MSBuild;
using Shouldly;
using Xunit;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using SemVeyor.Domain;
using SemVeyor.Tests.TestUtils;


namespace SemVeyor.Tests.Scanning.SourceCode.Queries
{
	public class GetTypeQueryTests
	{
		private readonly ClassDeclarationSyntax _class;

		public GetTypeQueryTests()
		{
			_class = LoadTestType().Result;
		}

		private static async Task<ClassDeclarationSyntax> LoadTestType()
		{

			var ws = MSBuildWorkspace.Create();
			var project = await ws.OpenProjectAsync(GetPath());
			var compilation = await project.GetCompilationAsync();

			return compilation
				.SyntaxTrees
				.SelectMany(tree => tree.GetRoot().DescendantNodesAndSelf().Where(t => t.IsKind(SyntaxKind.ClassDeclaration)))
				.Cast<ClassDeclarationSyntax>()
				.Where(cd => cd.Identifier.ValueText == nameof(TestType))
				.Single();
		}

		[Fact]
		public void When_loading_a_type()
		{
			_class.ShouldNotBeNull();
		}

		private static string GetPath([CallerFilePath]string filepath = "")
		{
			var search = ".Tests\\";
			var projectDirectory = filepath.Substring(0, filepath.IndexOf(search) + search.Length);

			return Path.Combine(projectDirectory, "SemVeyor.Tests.csproj");
		}
	}
}