using System.Collections.Generic;
using System.Linq;

namespace SemVeyor.CommandLine
{
	public class CliArgs
	{
		private readonly List<string> _paths;
		private readonly Dictionary<string, Dictionary<string, string>> _prefixes;
		private readonly Dictionary<string, HashSet<string>> _flags;

		public Dictionary<string, string> Arguments { get; }
		public IEnumerable<string> Paths => _paths;
		public IEnumerable<string> Prefixes => _prefixes.Keys;
		public HashSet<string> AllFlags => new HashSet<string>(_flags.SelectMany(pair =>
		{
			var prefix = pair.Key == string.Empty ? "" : pair.Key + ":";
			return pair.Value.Select(value => prefix + value);
		}));

		public CliArgs()
			: this(new Dictionary<string, string>(), new Dictionary<string, Dictionary<string, string>>(), new Dictionary<string, HashSet<string>>(),  new List<string>())
		{

		}

		public CliArgs(Dictionary<string, string> arguments, Dictionary<string, Dictionary<string, string>> prefixes, Dictionary<string, HashSet<string>> flags, List<string> paths)
		{
			Arguments = arguments;
			_paths = paths;
			_prefixes = prefixes;
			_flags = flags;
		}

		public Dictionary<string, string> ForPrefix(string prefix)
		{
			return _prefixes.ContainsKey(prefix)
				? _prefixes[prefix]
				: new Dictionary<string, string>();
		}
	}
}
