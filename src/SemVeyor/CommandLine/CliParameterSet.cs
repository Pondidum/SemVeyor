using System;
using System.Collections.Generic;

namespace SemVeyor.CommandLine
{
	public class CliParameterSet
	{
		public IDictionary<string, string> Arguments { get; set; }
		public ISet<string> Flags { get; set; }
		public IEnumerable<string> Paths { get; set; }

		public CliParameterSet()
		{
			Arguments = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
			Flags = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
			Paths = new List<string>();
		}
	}
}
