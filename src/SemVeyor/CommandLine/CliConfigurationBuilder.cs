using System.Collections.Generic;
using SemVeyor.Config;

namespace SemVeyor.CommandLine
{
	public class CliConfigurationBuilder
	{
		public Configuration Build(CliParameters cli)
		{
			var parameterSet = cli.ForPrefix("");

			var options = new Options
			{
				ReadOnly = parameterSet.Flags.Contains("readonly"),
				Paths = cli.Paths
			};

			var storage = new Dictionary<string, IDictionary<string, string>>();

			if (parameterSet.Arguments.ContainsKey("storage"))
				storage.Add(parameterSet.Arguments["storage"], new Dictionary<string, string>());

			var reporters = new Dictionary<string, IDictionary<string, string>>();

			if (parameterSet.Arguments.ContainsKey("reporting"))
				reporters.Add(parameterSet.Arguments["reporting"], new Dictionary<string, string>());

			return new Configuration(
				options,
				storage,
				reporters
			);
		}
	}
}
