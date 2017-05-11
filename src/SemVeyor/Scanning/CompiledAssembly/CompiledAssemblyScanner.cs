using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using SemVeyor.Domain;
using SemVeyor.Scanning.CompiledAssembly.Queries;

namespace SemVeyor.Scanning.CompiledAssembly
{
	public class CompiledAssemblyScanner : IAssemblyScanner
	{
		public Task<AssemblyDetails> Execute(AssemblyScannerArgs args)
		{
			var assembly = Assembly.LoadFile(args.Path);
			var getAllTypesQuery = new GetAllTypesQuery(new GetTypeQuery());

			return Task.FromResult(new AssemblyDetails
			{
				Name = assembly.FullName,
				Types = getAllTypesQuery.Execute(assembly).ToArray()
			});
		}
	}
}
