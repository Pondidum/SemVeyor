﻿using System;
using System.IO;
using System.Linq;
using SemVeyor.Configuration;
using SemVeyor.Scanning.CompiledAssembly;
using SemVeyor.Scanning.SourceCode;

namespace SemVeyor.Scanning
{
	public class ScannerFactory
	{
		public IAssemblyScanner CreateScanner(Options options)
		{
			var path = options.Paths.SingleOrDefault();

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
