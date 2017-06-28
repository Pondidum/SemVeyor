using System.Collections.Generic;
using SemVeyor.Config;

namespace SemVeyor.CommandLine
{
	public class CliConfigurationBuilder
	{
		public Configuration Build(CliParameters cli)
		{
			var parameterSet = cli.ForPrefix("");
			var options = parameterSet.Build<Options>();

			var storage = new Dictionary<string, IDictionary<string, string>>();

			if (parameterSet.Arguments.ContainsKey("storage"))
				storage.Add(parameterSet.Arguments["storage"], new Dictionary<string, string>());

			var reporters = new Dictionary<string, IDictionary<string, string>>();

			return new Configuration(
				options,
				storage,
				reporters
			);
		}
	}
}