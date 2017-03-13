using System;
using System.Collections.Generic;

namespace SemVeyor.CommandLine
{
	public class CliParameters
	{
		public Dictionary<string, CliParameterSet> Sets { get; }
		public List<string> Paths { get; }

		public CliParameters()
		{
			Sets = new Dictionary<string, CliParameterSet>(StringComparer.OrdinalIgnoreCase);
			Paths = new List<string>();
		}

		public CliParameterSet ForPrefix(string prefix)
		{
			if (Sets.ContainsKey(prefix) == false)
				Sets[prefix] = new CliParameterSet();

			return Sets[prefix];
		}
	}
}
