using System.Threading.Tasks;
using SemVeyor.Domain;

namespace SemVeyor.Scanning
{
	public interface IAssemblyScanner
	{
		Task<AssemblyDetails> Execute(AssemblyScannerArgs args);
	}

	public class AssemblyScannerArgs
	{
		public string Path { get; set; }
	}
}
