using System;
using System.Collections.Generic;
using System.Linq;

namespace SemVeyor.CommandLine
{
	public class Cli
	{
		private readonly Dictionary<string, string> _arguments;
		private readonly Dictionary<string, Dictionary<string, string>> _prefixes;
		private readonly List<string> _paths;

		public Cli()
		{
			_arguments = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
			_prefixes = new Dictionary<string, Dictionary<string, string>>(StringComparer.OrdinalIgnoreCase);
			_paths = new List<string>();
		}

		public CliArgs Parse(string[] args)
		{
			var handlers = new Func<string, Queue<string>, bool>[]
			{
				HandleArgument,
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

			return new CliArgs(_arguments, _prefixes, _paths);
		}

		private bool HandlePath(string name, Queue<string> tokens)
		{
			if (name.StartsWith("-"))
				return false;

			_paths.Add(name);

			return true;
		}

		private bool HandleArgument(string name, Queue<string> tokens)
		{
			if (name.StartsWith("-") == false)
				return false;

			name = name.TrimStart('-');

			if (tokens.Any() == false || tokens.Peek().StartsWith("-"))
				return false;

			if (name.Contains(":"))
			{
				var prefix = name.Substring(0, name.IndexOf(":"));
				var suffix = name.Substring(name.IndexOf(":") + 1);

				if (_prefixes.ContainsKey(prefix) == false)
					_prefixes[prefix] = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

				_prefixes[prefix][suffix] = tokens.Dequeue();
			}
			else
				_arguments.Add(name, tokens.Dequeue());

			return true;
		}
	}
}