using System.IO;
using System.Linq;
using System.Text;
using FileSystem;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using SemVeyor.Configuration;
using Shouldly;
using Xunit;

namespace SemVeyor.Tests.Configuration
{

	public class ConfigFileReaderTests
	{
		private readonly IFileSystem _filesystem;
		private readonly ConfigFileReader _reader;
		private readonly string _path;

		public ConfigFileReaderTests()
		{
			_filesystem = Substitute.For<IFileSystem>();
			_reader = new ConfigFileReader(_filesystem);
			_path = ConfigFileReader.FilePaths.First();
		}

		private void FileContents(string text)
		{
			var contents = new MemoryStream(Encoding.UTF8.GetBytes(text));

			_filesystem.FileExists(_path).Returns(true);
			_filesystem.ReadFile(_path).Returns(contents);
		}

		private void ShouldBeDefault(Config config)
		{
			config.ShouldSatisfyAllConditions(
				() => config.GlobalOptions.Paths.ShouldBeEmpty(),
				() => config.GlobalOptions.ReadOnly.ShouldBeFalse(),
				() => config.StorageTypes.ShouldBe(new[] {Options.DefaultStorage}),
				() => config.ReporterTypes.ShouldBe(new[] {Options.DefaultReporter})
			);
		}

		[Fact]
		public void When_a_config_file_does_not_exist()
		{
			_filesystem.ReadFile(_path).Throws(new FileNotFoundException());

			Should.Throw<FileNotFoundException>(() => _reader.Read(_path));
		}

		[Fact]
		public void When_a_config_file_is_blank()
		{
			FileContents("");
			ShouldBeDefault(_reader.Read(_path));
		}

		[Fact]
		public void When_a_config_hasnt_got_any_keys()
		{
			FileContents("{}");
			ShouldBeDefault(_reader.Read(_path));
		}

		[Fact]
		public void When_a_config_has_properties()
		{
			FileContents(@"
{
	""readonly"": true,
	""paths"": [ ""some\\file\\path.dll"" ]
}");
			var config = _reader.Read(_path);

			config.ShouldSatisfyAllConditions(
				() => config.GlobalOptions.ReadOnly.ShouldBeTrue(),
				() => config.GlobalOptions.Paths.ShouldBe(new[] {"some\\file\\path.dll"}),
				() => config.StorageTypes.ShouldBe(new[] {Options.DefaultStorage})
			);
		}

		[Fact]
		public void When_a_config_file_has_a_storage_node_with_a_key()
		{
			FileContents(@"
{
	""readonly"": true,
	""paths"": [ ""some\\file\\path.dll"" ],
	""storage"": {
		""aws:s3"": { ""accesskey"": ""1234"", ""secretkey"": ""wat"" }
	}
}");
			var config = _reader.Read(_path);

			config.ShouldSatisfyAllConditions(
				() => config.GlobalOptions.ReadOnly.ShouldBeTrue(),
				() => config.GlobalOptions.Paths.ShouldBe(new[] {"some\\file\\path.dll"}),
				() => config.StorageTypes.ShouldBe(new[] {"aws:s3"})
			);
		}
	}
}
