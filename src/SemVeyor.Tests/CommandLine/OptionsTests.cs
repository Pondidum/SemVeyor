﻿using System.Collections.Generic;
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
			var parameters = new CliParameters();
			parameters.CreateSet("", p => { });

			var options = Options.From(parameters);

			options.ShouldSatisfyAllConditions(
				() => options.Storage.ShouldBe(Options.DefaultStorage),
				() => options.Paths.ShouldBeEmpty()
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

			var options = Options.From(parameters);

			options.Storage.ShouldBe("aws:s3");
		}

		[Fact]
		public void When_a_path_is_specified()
		{
			var parameters = new CliParameters();
			parameters.Paths.Add("some/path.json");

			var options = Options.From(parameters);

			options.Paths.ShouldBe(new[] { "some/path.json" });
		}

	}
}