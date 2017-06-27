using System.Collections.Generic;
using System.Linq;
using SemVeyor.CommandLine;
using SemVeyor.Config;
using Shouldly;
using Xunit;

namespace SemVeyor.Tests.CommandLine
{
	public class OptionsTests
	{
		private static Configuration Build(CliParameters cli)
		{
			return new CliConfigurationBuilder().Build(cli);
		}

		[Fact]
		public void When_nothing_is_specified()
		{
			var parameters = new CliParameters();
			parameters.CreateSet("", p => { });

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
			parameters.CreateSet("", p =>
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
