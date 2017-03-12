using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;

namespace SemVeyor.CommandLine
{
	public class Cli
	{
		private readonly Dictionary<string, HashSet<string>> _flags;
		private readonly Dictionary<string, string> _arguments;
		private readonly Dictionary<string, Dictionary<string, string>> _prefixes;
		private readonly List<string> _paths;

		public Cli()
		{
			_flags = new Dictionary<string, HashSet<string>>(StringComparer.OrdinalIgnoreCase);
			_arguments = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
			_prefixes = new Dictionary<string, Dictionary<string, string>>(StringComparer.OrdinalIgnoreCase);
			_paths = new List<string>();
		}

		public CliArgs Parse(string[] args)
		{
			var handlers = new Func<string, Queue<string>, bool>[]
			{
				HandleArgument,
				HandleFlag,
				HandlePath
			};

			var queue = new Queue<string>(args);

			while (queue.Any())
			{
				var current = queue.Dequeue();
				var handler = handlers.FirstOrDefault(h => h(current, queue));

				if (handler == null)
					throw new NotSupportedException(current);
			}

			return new CliArgs(_arguments, _prefixes, _flags, _paths);
		}

		private bool HandleFlag(string name, Queue<string> tokens)
		{
			if (name.StartsWith("-") == false)
				return false;

			if (name == "--")
				return false;

			name = name.TrimStart('-');
			var index = name.IndexOf(":");

			var prefix = index >= 0 ? name.Substring(0, index) : string.Empty;
			var suffix = index >= 0 ? name.Substring(index + 1) : name;

			if (_flags.ContainsKey(prefix) == false)
				_flags[prefix] = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

			_flags[prefix].Add(suffix);

			return true;
		}

		private bool HandlePath(string name, Queue<string> tokens)
		{
			if (name == "--")
			{
				while (tokens.Any())
					_paths.Add(tokens.Dequeue());
				return true;
			}

			if (name.StartsWith("-"))
				return false;

			_paths.Add(name);

			return true;
		}

		private bool HandleArgument(string name, Queue<string> tokens)
		{
			if (name.StartsWith("-") == false)
				return false;

			if (name == "--")
				return false;

			name = name.TrimStart('-');

			if (tokens.Any() == false || tokens.Peek().StartsWith("-"))
				return false;

			var value = tokens.Dequeue();

			if (name.Contains(":"))
			{
				var prefix = name.Substring(0, name.IndexOf(":"));
				var suffix = name.Substring(name.IndexOf(":") + 1);

				if (_prefixes.ContainsKey(prefix) == false)
					_prefixes[prefix] = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

				_prefixes[prefix][suffix] = value;
			}
			else
				_arguments.Add(name, value);

			return true;
		}
	}
}