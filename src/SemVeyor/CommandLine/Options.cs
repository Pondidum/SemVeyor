using System.Collections.Generic;
using System.Linq;
using SemVeyor.Infrastructure;

namespace SemVeyor.CommandLine
{
	public class Options
	{
		public const string DefaultStorage = "file";

		public string Storage { get; }
		public IEnumerable<string> Assemblies { get; }

		public Options(CliParameterSet parameterSet)
		{
			Storage = parameterSet.Arguments.GetOrDefault("storage", "file");
			Assemblies = parameterSet.Paths.ToArray();
		}

		public static Options From(CliParameters cli) => new Options(cli.ForPrefix(""));
	}
}
