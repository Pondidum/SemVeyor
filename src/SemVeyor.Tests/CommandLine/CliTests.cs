using System.Collections.Generic;
using SemVeyor.CommandLine;
using Shouldly;
using Xunit;

namespace SemVeyor.Tests.CommandLine
{
	public class CliTests
	{
		private readonly CliParameters _cli;

		public CliTests()
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
				"-runnable",
				"--",
				"/path/to/assembly.dll",
			};

			_cli = new Cli().Parse(args);
		}

		[Fact]
		public void Arguments_should_be_populated()
		{
			_cli.ForPrefix("").Arguments.ShouldBe(new Dictionary<string, string>
			{
				{ "storage", "aws:s3" },
				{ "runnable", "true" }
			});

			_cli.ForPrefix("aws:s3").Arguments.ShouldBe(new Dictionary<string, string>
			{
				{ "accesskey", "123456" },
				{ "secretkey", "something with spaces" },
				{ "enable", "true" }
			});
		}

		[Fact]
		public void Paths_should_be_populated()
		{
			_cli.Paths.ShouldBe(new[]
			{
				"/path/to/assembly.dll"
			});
		}

		[Fact]
		public void Flags_are_populated()
		{
			_cli.ForPrefix("").Arguments.ShouldContainKey("runnable");
			_cli.ForPrefix("aws:s3").Arguments.ShouldContainKey("enable");
		}
	}
}
