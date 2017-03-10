using System.Collections.Generic;
using System.Linq;
using SemVeyor.CommandLine;
using Shouldly;
using Xunit;

namespace SemVeyor.Tests.CommandLine
{
	public class OptionsTests
	{
		[Fact]
		public void When_nothing_is_specified()
		{
			var options = new Options(
				new Dictionary<string, string>(),
				Enumerable.Empty<string>());

			options.ShouldSatisfyAllConditions(
				() => options.Storage.ShouldBe(Options.DefaultStorage),
				() => options.Assemblies.ShouldBeEmpty()
			);
		}

		[Fact]
		public void When_storage_is_specified()
		{
			var options = new Options(
				new Dictionary<string, string> { { "storage", "aws:s3" } },
				Enumerable.Empty<string>());

			options.Storage.ShouldBe("aws:s3");
		}

		[Fact]
		public void When_a_path_is_specified()
		{
			var options = new Options(
				new Dictionary<string, string>(),
				new[] { "some/path.json" });

			options.Assemblies.ShouldBe(new[] { "some/path.json" });
		}

	}
}
