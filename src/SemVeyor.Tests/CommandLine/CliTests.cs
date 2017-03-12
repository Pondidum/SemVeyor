using System.Collections.Generic;
using SemVeyor.CommandLine;
using Shouldly;
using Xunit;

namespace SemVeyor.Tests.CommandLine
{
	public class CliTests
	{
		private readonly CliArgs _cli;

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
			_cli.Arguments.ShouldBe(new Dictionary<string, string>
			{
				{ "storage", "aws:s3" }
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
		public void Prefix_should_be_populated()
		{
			_cli.ForPrefix("storage").ShouldBe(new Dictionary<string, string>
			{
				{ "accesskey", "123456" },
				{ "secretkey", "something with spaces" }
			});
		}

		[Fact]
		public void All_prefixes_should_be_listed()
		{
			_cli.Prefixes.ShouldBe(new[]
			{
				"storage"
			});
		}

		[Fact]
		public void Flags_are_populated()
		{
			_cli.AllFlags.ShouldBe(new[]
			{
				"runnable",
				"storage:enable"
			}, ignoreOrder: true);
		}
	}
}