using System.Reflection;
using SemVeyor.Domain;
using SemVeyor.Domain.Queries;

namespace SemVeyor.Scanning
{
	public class CompiledAssemblyScanner : IAssemblyScanner
	{
		public AssemblyDetails Execute(AssemblyScannerArgs args)
		{
			return new GetAssemblyQuery().Execute(Assembly.LoadFile(args.Path));
		}
	}
}
