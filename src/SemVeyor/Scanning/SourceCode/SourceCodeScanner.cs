using System.Threading.Tasks;
using Microsoft.CodeAnalysis.MSBuild;
using SemVeyor.Domain;
using SemVeyor.Scanning.SourceCode.Queries;

namespace SemVeyor.Scanning.SourceCode
{
	public class SourceCodeScanner : IAssemblyScanner
	{
		public async Task<AssemblyDetails> Execute(AssemblyScannerArgs args)
		{
			var ws = MSBuildWorkspace.Create();
			var project = await ws.OpenProjectAsync(args.Path);
			var compilation = await project.GetCompilationAsync();

			var query = new GetAllTypesQuery(new GetTypeQuery());

			return new AssemblyDetails
			{
				Name = compilation.AssemblyName,
				Types = query.Execute(compilation)
			};
		}
	}
}