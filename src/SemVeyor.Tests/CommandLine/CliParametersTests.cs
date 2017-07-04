using System;
using System.Collections.Generic;
using SemVeyor.CommandLine;
using Shouldly;
using Xunit;

namespace SemVeyor.Tests.CommandLine
{
	public class CliParametersTests
	{
		private readonly CliParameters _all;

		private void CreateSet(string prefix, Action<CliParameterSet> customise) => customise(_all.ForPrefix(prefix));

		public CliParametersTests()
		{
			_all = new CliParameters();
			_all.Paths.AddRange(new[] { "/the/first/assembly.dll", "/the/second/executable.exe" });

			CreateSet("", set =>
			{
				set.Arguments = new Dictionary<string, string>
				{
					{ "storage", "s3" },
					{ "readonly", "true"}
				};
			});

			CreateSet("s3", set =>
			{
				set.Arguments = new Dictionary<string, string>
				{
					{ "access_key", "123456" },
					{ "secret_key", "987654" },
					{ "enable-mfa", "true"}
				};
			});
		}

		[Fact]
		public void An_ivalid_prefix_arguments_are_populated()
		{
			_all.ForPrefix("Wat").Arguments.ShouldBeEmpty();
		}

		[Fact]
		public void An_ivalid_prefix_paths_are_populated()
		{
			_all.ForPrefix("Wat").Paths.ShouldBe(_all.Paths);
		}

		[Fact]
		public void A_valid_prefix_arguments_are_populated()
		{
			_all.ForPrefix("").Arguments.ShouldBe(new Dictionary<string, string>
			{
				{ "storage", "s3"},
				{ "readonly", "true"}
			});
		}

		[Fact]
		public void A_valid_prefix_flags_are_populated()
		{
			_all.ForPrefix("").Arguments.ShouldContainKey("readonly");
		}

		[Fact]
		public void A_valid_prefix_paths_are_populated()
		{
			_all.ForPrefix("").Paths.ShouldBe(_all.Paths);
		}
	}
}
