using System;
using System.IO;
using System.Linq;
using SemVeyor.CommandLine;
using SemVeyor.Scanning.CompiledAssembly;

namespace SemVeyor.Scanning
{
	public class ScannerFactory
	{
		public IAssemblyScanner CreateScanner(CliParameters cli, Options options)
		{
			var path = cli.Paths.SingleOrDefault();

			if (IsAssembly(path))
				return new CompiledAssemblyScanner();

			if (IsProject(path))
				return new SourceCodeScanner();

			throw new NotSupportedException();
		}

		private static bool IsAssembly(string path)
		{
			var extensions = new[] { ".dll", ".exe" };

			return extensions.Contains(
				Path.GetExtension(path),
				StringComparer.OrdinalIgnoreCase);
		}

		private static bool IsProject(string path)
		{
			return string.Equals(
				Path.GetExtension(path),
				".csproj",
				StringComparison.OrdinalIgnoreCase);
		}
	}
}
