using System.Collections.Generic;
using SemVeyor.Configuration;

namespace SemVeyor.CommandLine
{
	public class CliConfigurationBuilder
	{
		public Configuration.Config Build(CliParameters cli)
		{
			var parameterSet = cli.ForPrefix("");

			var options = new Options
			{
				ReadOnly = parameterSet.Arguments.ContainsKey("readonly"),
				Paths = cli.Paths
			};

			var storage = new Dictionary<string, IDictionary<string, string>>();

			if (parameterSet.Arguments.ContainsKey("storage"))
			{
				var key = parameterSet.Arguments["storage"];
				storage.Add(key, cli.ForPrefix(key).Arguments);
			}

			var reporters = new Dictionary<string, IDictionary<string, string>>();

			if (parameterSet.Arguments.ContainsKey("reporting"))
			{
				var key = parameterSet.Arguments["reporting"];
				reporters.Add(key, cli.ForPrefix(key).Arguments);
			}

			return new Configuration.Config(
				options,
				storage,
				reporters
			);
		}
	}
}
