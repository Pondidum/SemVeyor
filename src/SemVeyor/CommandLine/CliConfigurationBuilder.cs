using SemVeyor.Config;

namespace SemVeyor.CommandLine
{
	public class CliConfigurationBuilder
	{
		public Configuration Build(CliParameters cli)
		{
			var options = cli.ForPrefix("").Build<Options>();
			
			return new Configuration(
				options
			);
		}
	}
}
