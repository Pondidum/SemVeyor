using System;
using System.Collections.Generic;

namespace SemVeyor.CommandLine
{
	public class CliParameterSet
	{
		public IDictionary<string, string> Arguments { get; set; }
		public IEnumerable<string> Paths { get; }

		public CliParameterSet(IEnumerable<string> paths)
		{
			Arguments = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
			Paths = paths;
		}
	}
}
