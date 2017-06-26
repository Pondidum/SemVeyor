namespace SemVeyor.Config
{
	public class Configuration
	{
		public Options GlobalOptions { get; }

		public Configuration(Options options)
		{
			GlobalOptions = options;
		}
	}
}
