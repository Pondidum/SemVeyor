using System;
using System.Collections.Generic;
using SemVeyor.CommandLine;
using Shouldly;
using Xunit;

namespace SemVeyor.Tests.CommandLine
{
	public class CliParamerterSetTests
	{
		private readonly CliParameterSet _parameters;

		public CliParamerterSetTests()
		{
			_parameters = new CliParameterSet(new []{ "/first/path.dll", "/second/path.exe" })
			{
				Arguments =
				{
					{ "storage", "s3" },
					{ "report", "tree" }
				},
				Flags =
				{
					"readonly"
				}
			};
		}

		[Fact]
		public void When_mapping_to_an_object_with_public_properties()
		{
			var options = _parameters.Build<PublicSetters>();

			options.ShouldSatisfyAllConditions(
				() => options.Paths.ShouldBe(_parameters.Paths),
				() => options.Storage.ShouldBe("s3"),
				() => options.Report.ShouldBe(Reports.Tree),
				() => options.ReadOnly.ShouldBe(true),
				() => options.TestRun.ShouldBe(false)
			);
		}

		[Fact]
		public void When_mapping_to_an_object_with_private_setter_properties()
		{
			var options = _parameters.Build<PrivateSetters>();

			options.ShouldSatisfyAllConditions(
				() => options.Paths.ShouldBe(_parameters.Paths),
				() => options.Storage.ShouldBe("s3"),
				() => options.Report.ShouldBe(Reports.Tree),
				() => options.ReadOnly.ShouldBe(true),
				() => options.TestRun.ShouldBe(false)
			);
		}

		private class PublicSetters
		{
			public string Storage { get; set; }
			public Reports Report { get; set; }
			public bool ReadOnly { get; set; }
			public bool TestRun { get; set; }

			public IEnumerable<string> Paths { get; set; }
		}

		private class PrivateSetters
		{
			public string Storage { get; private set; }
			public Reports Report { get; private set; }
			public bool ReadOnly { get; private set; }
			public bool TestRun { get; private set; }

			public IEnumerable<string> Paths { get; private set; }
		}

		private enum Reports
		{
			None,
			Flat,
			Tree,
			Hybrid
		}
	}
}
