using System;
using System.Collections.Generic;

namespace SemVeyor.CommandLine
{
	public class CliParameters
	{
		public IEnumerable<CliParameterSet> Sets => _sets.Values;
		public List<string> Paths { get; }

		private readonly Dictionary<string, CliParameterSet> _sets;

		public CliParameters()
		{
			_sets = new Dictionary<string, CliParameterSet>(StringComparer.OrdinalIgnoreCase);
			Paths = new List<string>();
		}

		public CliParameterSet ForPrefix(string prefix)
		{
			if (_sets.ContainsKey(prefix) == false)
				_sets[prefix] = new CliParameterSet(Paths);

			return _sets[prefix];
		}
	}
}
