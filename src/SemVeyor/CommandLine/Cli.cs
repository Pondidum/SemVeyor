﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace SemVeyor.CommandLine
{
	public class Cli
	{
		public CliArgs Parse(IEnumerable<string> args)
		{
			var handlers = new Func<CliArgs, string, Queue<string>, bool>[]
			{
				HandleArgument,
				HandleFlag,
				HandlePath
			};

			var dto = new CliArgs();
			var queue = new Queue<string>(args);

			while (queue.Any())
			{
				var current = queue.Dequeue();
				var handler = handlers.FirstOrDefault(h => h(dto, current, queue));

				if (handler == null)
					throw new NotSupportedException(current);
			}

			return dto;
		}

		private bool HandleFlag(CliArgs dto, string name, Queue<string> tokens)
		{
			if (name.StartsWith("-") == false)
				return false;

			if (name == "--")
				return false;

			name = name.TrimStart('-');
			var index = name.IndexOf(":");

			var prefix = index >= 0 ? name.Substring(0, index) : string.Empty;
			var suffix = index >= 0 ? name.Substring(index + 1) : name;

			if (dto.Flags.ContainsKey(prefix) == false)
				dto.Flags[prefix] = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

			dto.Flags[prefix].Add(suffix);

			return true;
		}

		private bool HandlePath(CliArgs dto, string name, Queue<string> tokens)
		{
			if (name == "--")
			{
				while (tokens.Any())
					dto.Paths.Add(tokens.Dequeue());

				return true;
			}

			if (name.StartsWith("-"))
				return false;

			dto.Paths.Add(name);

			return true;
		}

		private bool HandleArgument(CliArgs dto, string name, Queue<string> tokens)
		{
			if (name.StartsWith("-") == false)
				return false;

			if (name == "--")
				return false;

			name = name.TrimStart('-');

			if (tokens.Any() == false || tokens.Peek().StartsWith("-"))
				return false;

			var value = tokens.Dequeue();

			var index = name.IndexOf(":");

			var prefix = index >= 0 ? name.Substring(0, index) : string.Empty;
			var suffix = index >= 0 ? name.Substring(index + 1) : name;

			if (dto.Arguments.ContainsKey(prefix) == false)
				dto.Arguments[prefix] = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

			dto.Arguments[prefix][suffix] = value;

			return true;
		}
	}
}
