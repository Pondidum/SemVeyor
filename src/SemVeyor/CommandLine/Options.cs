using System.Collections.Generic;
using System.Linq;
using SemVeyor.Infrastructure;

namespace SemVeyor.CommandLine
{
	public class Options
	{
		public const string DefaultStorage = "file";

		public string Storage { get; private set;  }
		public IEnumerable<string> Paths { get; private set; }
		public string Reporter { get; private set; }

		public Options()
		{
			Storage = "file";
			Paths = Enumerable.Empty<string>();
		}

		public static Options From(CliParameters parameters) => parameters.ForPrefix("").Build<Options>();
	}
}
