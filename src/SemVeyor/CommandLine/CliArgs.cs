using System.Collections.Generic;

namespace SemVeyor.CommandLine
{
	public class CliArgs
	{
		private readonly List<string> _paths;
		private readonly Dictionary<string, Dictionary<string, string>> _prefixes;

		public Dictionary<string, string> Arguments { get; }
		public IEnumerable<string> Paths => _paths;
		public IEnumerable<string> Prefixes => _prefixes.Keys;

		public CliArgs()
			: this(new Dictionary<string, string>(), new Dictionary<string, Dictionary<string, string>>(), new List<string>())
		{

		}

		public CliArgs(Dictionary<string, string> arguments, Dictionary<string, Dictionary<string, string>> prefixes, List<string> paths)
		{
			Arguments = arguments;
			_paths = paths;
			_prefixes = prefixes;
		}

		public Dictionary<string, string> ForPrefix(string prefix)
		{
			return _prefixes.ContainsKey(prefix)
				? _prefixes[prefix]
				: new Dictionary<string, string>();
		}
	}
}
