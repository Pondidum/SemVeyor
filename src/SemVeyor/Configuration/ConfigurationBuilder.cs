using System;
using System.IO;
using System.Linq;
using FileSystem;
using SemVeyor.Commands.Scan;

namespace SemVeyor.Configuration
{
	public class ConfigurationBuilder
	{
		private static readonly string DefaultConfigFile = ConfigFileReader.FilePaths.First();
		
		private readonly IFileSystem _fileSystem;
		private readonly ConfigFileReader _configReader;

		public ConfigurationBuilder(IFileSystem fileSystem)
		{
			_fileSystem = fileSystem;
			_configReader = new ConfigFileReader(_fileSystem);
		}

		public Config Build(ScanInput commandLine)
		{
			if (string.IsNullOrWhiteSpace(commandLine.ConfigFlag) == false)
			{
				if (_fileSystem.FileExists(commandLine.ConfigFlag).Result)
				{
					return _configReader.Read(commandLine.ConfigFlag).OverrideWith(commandLine.AsOptions());
				}
				else
				{
					throw new FileNotFoundException("Could not find config file.", commandLine.ConfigFlag);
				}
			}

			if (string.IsNullOrWhiteSpace(commandLine.ConfigFlag))
			{
				if (_fileSystem.FileExists(DefaultConfigFile).Result == false)
				{
					if (string.IsNullOrWhiteSpace(commandLine.Path) == false)
					{
						return new Config().OverrideWith(commandLine.AsOptions());
					}
					else
					{
						throw new FileNotFoundException("Could not find config file.", commandLine.ConfigFlag);
					}
				}
				else
				{
					return _configReader.Read(DefaultConfigFile).OverrideWith(commandLine.AsOptions());
				}
			}
			
			throw new NotSupportedException();
		}
	}
}
