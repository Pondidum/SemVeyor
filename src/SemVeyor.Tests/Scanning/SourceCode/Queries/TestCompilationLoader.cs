using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.MSBuild;

namespace SemVeyor.Tests.Scanning.SourceCode.Queries
{
	internal class TestCompilationLoader
	{
		private static string GetPath([CallerFilePath]string filepath = "")
		{
			var search = ".Tests\\";
			var projectDirectory = filepath.Substring(0, filepath.IndexOf(search) + search.Length);

			return Path.Combine(projectDirectory, "SemVeyor.Tests.csproj");
		}

		private static async Task<Compilation> Load()
		{
			var ws = MSBuildWorkspace.Create();
			var project = await ws.OpenProjectAsync(GetPath());
			var compilation = await project.GetCompilationAsync();

			return compilation;
		}

		private static readonly Lazy<Compilation> Compilation = new Lazy<Compilation>(() => Load().Result);

		public static Compilation Get() => Compilation.Value;
	}
}
