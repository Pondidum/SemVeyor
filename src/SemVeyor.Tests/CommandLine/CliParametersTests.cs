using System.Collections.Generic;
using SemVeyor.CommandLine;
using Shouldly;
using Xunit;

namespace SemVeyor.Tests.CommandLine
{
	public class CliParametersTests
	{
		private readonly CliParameters _all;

		public CliParametersTests()
		{
			_all = new CliParameters();
			_all.Paths.AddRange(new[] { "/the/first/assembly.dll", "/the/second/executable.exe" });

			_all.CreateSet("", set =>
			{
				set.Arguments = new Dictionary<string, string>
				{
					{ "storage", "s3" }
				};

				set.Flags = new HashSet<string> { "readonly" };
			});

			_all.CreateSet("s3", set =>
			{
				set.Arguments = new Dictionary<string, string>
				{
					{ "access_key", "123456" },
					{ "secret_key", "987654" }
				};

				set.Flags = new HashSet<string> { "enable-mfa" };
			});
		}

		[Fact]
		public void An_ivalid_prefix_arguments_are_populated()
		{
			_all.ForPrefix("Wat").Arguments.ShouldBeEmpty();
		}

		[Fact]
		public void An_ivalid_prefix_flags_are_populated()
		{
			_all.ForPrefix("Wat").Flags.ShouldBeEmpty();
		}

		[Fact]
		public void An_ivalid_prefix_paths_are_populated()
		{
			_all.ForPrefix("Wat").Paths.ShouldBe(_all.Paths);
		}

		[Fact]
		public void A_valid_prefix_arguments_are_populated()
		{
			_all.ForPrefix("").Arguments.ShouldBe(new Dictionary<string, string> { { "storage", "s3"} });
		}

		[Fact]
		public void A_valid_prefix_flags_are_populated()
		{
			_all.ForPrefix("").Flags.ShouldBe(new[] { "readonly" });
		}

		[Fact]
		public void A_valid_prefix_paths_are_populated()
		{
			_all.ForPrefix("").Paths.ShouldBe(_all.Paths);
		}
	}
}
