using System;
using System.Collections.Generic;
using SemVeyor.CommandLine;
using SemVeyor.Configuration;
using Shouldly;
using Xunit;

namespace SemVeyor.Tests.CommandLine
{
	public class OptionsTests
	{
		private static Config Build(CliParameters cli) => new CliConfigurationBuilder().Build(cli);
		private static void CreateSet(CliParameters parameters, string prefix, Action<CliParameterSet> customise) => customise(parameters.ForPrefix(prefix));


		[Fact]
		public void When_nothing_is_specified()
		{
			var parameters = new CliParameters();
			CreateSet(parameters, "", p => { });

			var config = Build(parameters);

			config.ShouldSatisfyAllConditions(
				() => config.StorageTypes.ShouldContain(Options.DefaultStorage),
				() => config.GlobalOptions.Paths.ShouldBeEmpty()
			);
		}

		[Fact]
		public void When_storage_is_specified()
		{
			var parameters = new CliParameters();
			CreateSet(parameters, "", p =>
			{
				p.Arguments = new Dictionary<string, string> { { "storage", "aws:s3" } };
			});

			var config = Build(parameters);

			config.StorageTypes.ShouldBe(new [] {"aws:s3"});
		}

		[Fact]
		public void When_a_path_is_specified()
		{
			var parameters = new CliParameters();
			parameters.Paths.Add("some/path.json");

			var config = Build(parameters);

			config.GlobalOptions.Paths.ShouldBe(new[] { "some/path.json" });
		}

	}
}
