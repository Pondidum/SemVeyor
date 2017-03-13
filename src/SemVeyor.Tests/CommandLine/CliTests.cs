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
				"-storage:accesskey",
				"123456",
				"-storage:secretkey",
				"something with spaces",
				"-storage:enable",
				"-runnable",
				"--",
				"/path/to/assembly.dll",
			};

			_cli = new Cli().Parse(args);
		}

		[Fact]
		public void Arguments_should_be_populated()
		{
			_cli.Arguments[""].ShouldBe(new Dictionary<string, string>
			{
				{ "storage", "aws:s3" }
			});

			_cli.Arguments["storage"].ShouldBe(new Dictionary<string, string>
			{
				{ "accesskey", "123456" },
				{ "secretkey", "something with spaces" }
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
			_cli.Flags[""].ShouldBe(new HashSet<string> { "runnable" });
			_cli.Flags["storage"].ShouldBe(new HashSet<string> { "enable" });
		}
	}
}
