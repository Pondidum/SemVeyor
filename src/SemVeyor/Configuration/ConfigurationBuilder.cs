using FileSystem;
using SemVeyor.Commands.Scan;

namespace SemVeyor.Configuration
{
	public class ConfigurationBuilder
	{
		private readonly IFileSystem _fileSystem;

		public ConfigurationBuilder(IFileSystem fileSystem)
		{
			_fileSystem = fileSystem;
		}

		public Config Build(ScanInput commandLine)
		{
			var reader = new ConfigFileReader(_fileSystem);
			var configuration = reader
				.Read()
				.OverrideWith(commandLine.AsOptions());

			return configuration;
		}
	}
}
