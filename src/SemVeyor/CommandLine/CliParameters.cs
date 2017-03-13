using System;
using System.Collections.Generic;

namespace SemVeyor.CommandLine
{
	public class CliParameters
	{
		public Dictionary<string, Dictionary<string, string>> Arguments { get; }
		public List<string> Paths { get; }
		public Dictionary<string, HashSet<string>> Flags { get; }

		public CliParameters()
		{
			Arguments = new Dictionary<string, Dictionary<string, string>>(StringComparer.OrdinalIgnoreCase);
			Flags = new Dictionary<string, HashSet<string>>(StringComparer.OrdinalIgnoreCase);
			Paths = new List<string>();
		}

		public CliParameters ForPrefix(string prefix)
		{
			throw new NotImplementedException();
		}
	}
}
