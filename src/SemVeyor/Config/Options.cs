using System.Collections.Generic;
using System.Linq;

namespace SemVeyor.Config
{
	public class Options
	{
		public const string DefaultStorage = "file";
		public const string DefaultReporter = "simple";

		public bool ReadOnly { get; set; }
		public IEnumerable<string> Paths { get; set; }

		public Options()
		{
			ReadOnly = false;
			Paths = Enumerable.Empty<string>();
		}
	}
}
