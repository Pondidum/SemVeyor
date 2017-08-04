using System.IO;
using System.Linq;
using FileSystem;
using SemVeyor.Commands.Scan;

namespace SemVeyor.Configuration
{
	public class ConfigBuilder
	{
		private static readonly string DefaultConfigFile = ConfigFileReader.FilePaths.First();

		private readonly IFileSystem _fileSystem;
		private readonly ConfigFileReader _configReader;

		public ConfigBuilder(IFileSystem fileSystem)
		{
			_fileSystem = fileSystem;
			_configReader = new ConfigFileReader(_fileSystem);
		}

		private static bool CustomConfigSpecified(ScanInput input) => string.IsNullOrWhiteSpace(input.ConfigFlag) == false;
		private static bool TargetSpecified(ScanInput input) => string.IsNullOrWhiteSpace(input.Path) == false;

		public Config Build(ScanInput commandLine)
		{
			if (CustomConfigSpecified(commandLine))
				return ReadFile(commandLine.ConfigFlag, commandLine.AsOptions());

			if (TargetSpecified(commandLine))
				return new Config().OverrideWith(commandLine.AsOptions());

			return ReadFile(DefaultConfigFile, commandLine.AsOptions());
		}

		private Config ReadFile(string path, Options options)
		{
			if (_fileSystem.FileExists(path).Result)
				return _configReader.Read(path).OverrideWith(options);

			throw new FileNotFoundException("Could not find config file.", path);
		}
	}
}
