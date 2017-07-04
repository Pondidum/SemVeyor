using SemVeyor.CommandLine;
using SemVeyor.Configuration;
using Shouldly;
using Xunit;

namespace SemVeyor.Tests.CommandLine
{
	public class CliConfigurationBuilderTests
	{
		private readonly Config _config;

		public CliConfigurationBuilderTests()
		{
			var args = new[]
			{
				"-storage",
				"aws:s3",
				"-aws:s3:accesskey",
				"123456",
				"-aws:s3:secretkey",
				"something with spaces",
				"-aws:s3:enable",
				"-readonly",
				"--",
				"/path/to/assembly.dll",
			};

			var cli = new Cli().Parse(args);
			var builder = new CliConfigurationBuilder();

			_config = builder.Build(cli);
		}

		[Fact]
		public void Storage_should_contain_s3() => _config.StorageTypes.ShouldBe(new [] { "aws:s3" });

		[Fact]
		public void Aws_should_have_access_key_set() => _config.StorageOptions<AwsOptions>("aws:s3").AccessKey.ShouldBe(123456);

		[Fact]
		public void Aws_should_have_secret_key_set() => _config.StorageOptions<AwsOptions>("aws:s3").SecretKey.ShouldBe("something with spaces");

		[Fact]
		public void Aws_should_have_enabled_set() => _config.StorageOptions<AwsOptions>("aws:s3").Enable.ShouldBeTrue();

		[Fact]
		public void Readonly_should_be_set() => _config.GlobalOptions.ReadOnly.ShouldBeTrue();

		[Fact]
		public void Paths_should_contain_the_assembly() => _config.GlobalOptions.Paths.ShouldBe(new[] { "/path/to/assembly.dll" });

		private class AwsOptions
		{
			public int AccessKey { get; set; }
			public string SecretKey { get; set; }
			public bool Enable { get; set; }
		}
	}
}
