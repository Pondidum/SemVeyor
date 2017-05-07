using System.Collections.Generic;
using System.Linq;
using SemVeyor.Infrastructure;

namespace SemVeyor.CommandLine
{
	public class Options
	{
		public const string DefaultStorage = "file";
		public const string DefaultReporter = "simple";

		public string Storage { get; set;  }
		public IEnumerable<string> Paths { get; set; }
		public string Reporter { get; set; }

		public Options()
		{
			Storage = DefaultStorage;
			Reporter = DefaultReporter;
			Paths = Enumerable.Empty<string>();
		}

		public static Options From(CliParameters parameters) => parameters.ForPrefix("").Build<Options>();
	}
}
