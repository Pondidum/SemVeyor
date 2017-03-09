using System;
using System.Collections.Generic;
using Shouldly;
using Xunit;

namespace SemVeyor.Tests.CommandLine
{
	public class Scratch
	{
		private readonly Cli _cli;

		public Scratch()
		{
			var args = new[]
			{
				"-storage",
				"aws:s3",
				"-storage:accesskey",
				"123456",
				"-storage:secretkey",
				"something with spaces",
				"/path/to/assembly.dll",
			};

			_cli = new Cli(args);
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

		private class Cli
		{
			public Cli(string[] args)
			{
			}

			public Dictionary<string, string> Arguments { get; private set; }
			public IEnumerable<string> Paths { get; private set; }

			public Dictionary<string, string> ForPrefix(string prefix)
			{
				throw new NotImplementedException();
			}
		}
	}
}