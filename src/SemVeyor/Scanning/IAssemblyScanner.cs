using SemVeyor.Domain;

namespace SemVeyor.Scanning
{
	public interface IAssemblyScanner
	{
		AssemblyDetails Execute(AssemblyScannerArgs args);
	}

	public class AssemblyScannerArgs
	{
		public string Path { get; set; }
	}
}
