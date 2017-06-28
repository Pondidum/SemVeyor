using System.Collections.Generic;
using System.Linq;
using SemVeyor.CommandLine;

namespace SemVeyor.Config
{
	public class Options
	{
		public const string DefaultStorage = "file";
		public const string DefaultReporter = "simple";

		public bool ReadOnly { get; set; }
//		public string Storage { get; set;  }
		public IEnumerable<string> Paths { get; set; }
//		public string Reporter { get; set; }

		public Options()
		{
			ReadOnly = false;
//			Storage = DefaultStorage;
//			Reporter = DefaultReporter;
			Paths = Enumerable.Empty<string>();
		}
	}
}
