using System.Collections.Generic;
using System.Linq;
using SemVeyor.Classification;

namespace SemVeyor.Configuration
{
	public class Options
	{
		public const string DefaultStorage = "file";
		public const string DefaultReporter = "simple";

		public bool ReadOnly { get; set; }
		public IEnumerable<string> Paths { get; set; }
		public IDictionary<string, SemVer> Classifications { get; set; }

		public Options()
		{
			ReadOnly = false;
			Paths = Enumerable.Empty<string>();
			Classifications = EventClassification.DefaultClassificationMap;
		}
	}
}
