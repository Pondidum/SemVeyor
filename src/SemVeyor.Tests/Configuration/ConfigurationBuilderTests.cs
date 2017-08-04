using System.IO;
using System.Linq;
using System.Text;
using FileSystem;
using NSubstitute;
using SemVeyor.Commands.Scan;
using SemVeyor.Configuration;
using Shouldly;
using Xunit;

namespace SemVeyor.Tests.Configuration
{
	public class ConfigurationBuilderTests
	{
		private static readonly string DefaultConfigFile = ConfigFileReader.FilePaths.First();

		private readonly IFileSystem _fileSystem;
		private readonly ConfigBuilder _builder;

		public ConfigurationBuilderTests()
		{
			_fileSystem = Substitute.For<IFileSystem>();
			_builder = new ConfigBuilder(_fileSystem);
		}

		private void SetupFile(string path, string content = "{}")
		{
			_fileSystem.FileExists(path).Returns(true);
			_fileSystem.ReadFile(path).Returns(new MemoryStream(Encoding.UTF8.GetBytes(content)));
		}

		private void FileWasRead(string path) => _fileSystem.Received().ReadFile(path);
		private void FileWasNotRead(string path) => _fileSystem.DidNotReceive().ReadFile(path);

		[Fact]
		public void When_no_config_specified_and_default_file_doesnt_exist_and_no_target_specified()
		{
			Should.Throw<FileNotFoundException>(() => _builder.Build(new ScanInput()));

			FileWasNotRead(DefaultConfigFile);
		}

		[Fact]
		public void When_no_config_specified_and_default_file_doesnt_exist_and_a_target_is_specified()
		{
			var config = _builder.Build(new ScanInput { Path = "./test.csproj" });

			config.GlobalOptions.Paths.ShouldHaveSingleItem("./test.csproj");
			FileWasNotRead(DefaultConfigFile);
		}

		[Fact]
		public void When_no_config_specified_and_default_file_exists()
		{
			SetupFile(DefaultConfigFile);

			_builder.Build(new ScanInput()).ShouldNotBeNull();

			FileWasRead(DefaultConfigFile);
		}

		[Fact]
		public void When_a_config_is_specified_and_it_doesnt_exist()
		{
			Should.Throw<FileNotFoundException>(() => _builder.Build(new ScanInput
			{
				ConfigFlag = "non-existing.json"
			}));

			FileWasNotRead(DefaultConfigFile);
			FileWasNotRead("non-existing.json");
		}

		[Fact]
		public void When_a_config_is_specified_and_it_does_exist()
		{
			SetupFile(DefaultConfigFile);
			SetupFile("custom.json");

			_builder.Build(new ScanInput { ConfigFlag = "custom.json"}).ShouldNotBeNull();

			FileWasRead("custom.json");
			FileWasNotRead(DefaultConfigFile);
		}
	}
}
