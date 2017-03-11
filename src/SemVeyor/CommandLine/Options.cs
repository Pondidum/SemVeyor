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

		public Options(IDictionary<string, string> arguments, IEnumerable<string> paths)
		{
			Storage = arguments.GetOrDefault("storage", "file");
			Assemblies = paths.ToArray();
		}

		public static Options From(CliArgs cli) => new Options(cli.Arguments, cli.Paths);
	}
}
