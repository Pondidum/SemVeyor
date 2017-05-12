using System.Linq;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.MSBuild;
using SemVeyor.Domain;

namespace SemVeyor.Scanning.SourceCode
{
	public class SourceCodeScanner : IAssemblyScanner
	{
		public async Task<AssemblyDetails> Execute(AssemblyScannerArgs args)
		{
			var ws = MSBuildWorkspace.Create();
			var project = await ws.OpenProjectAsync(args.Path);
			var compilation = await project.GetCompilationAsync();


			var classes = compilation
				.SyntaxTrees
				.SelectMany(tree => tree.GetRoot().DescendantNodesAndSelf().Where(t => t.IsKind(SyntaxKind.ClassDeclaration)))
				.Cast<ClassDeclarationSyntax>()
				.Select(c => new TypeDetails
				{
					Name = c.Identifier.ValueText
				});

			return new AssemblyDetails
			{
				Name = compilation.AssemblyName,
				Types = classes
			};
		}
	}
}